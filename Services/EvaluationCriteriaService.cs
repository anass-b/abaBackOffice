using AutoMapper;
using abaBackOffice.DTOs;
using abaBackOffice.Interfaces;
using abaBackOffice.Interfaces.Services;
using abaBackOffice.Models;
using Microsoft.Extensions.Logging;

namespace abaBackOffice.Services
{
    public class EvaluationCriteriaService : IEvaluationCriteriaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<EvaluationCriteriaService> _logger;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public EvaluationCriteriaService(IUnitOfWork unitOfWork, ILogger<EvaluationCriteriaService> logger, IMapper mapper, IWebHostEnvironment env)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            _env = env;
        }

        public async Task<IEnumerable<EvaluationCriteriaDto>> GetAllAsync()
        {
            try
            {
                var entities = await _unitOfWork.EvaluationCriteriaRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<EvaluationCriteriaDto>>(entities);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving EvaluationCriterias");
                throw;
            }
        }

        public async Task<EvaluationCriteriaDto> GetByIdAsync(int id)
        {
            try
            {
                var entity = await _unitOfWork.EvaluationCriteriaRepository.GetByIdAsync(id);
                if (entity == null)
                    throw new KeyNotFoundException($"EvaluationCriteria with ID {id} not found.");
                return _mapper.Map<EvaluationCriteriaDto>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving EvaluationCriteria with id {id}");
                throw;
            }
        }

        public async Task<IEnumerable<EvaluationCriteriaDto>> GetByTaskIdAsync(int taskId)
        {
            try
            {
                var list = await _unitOfWork.EvaluationCriteriaRepository.GetByTaskIdAsync(taskId);
                return _mapper.Map<IEnumerable<EvaluationCriteriaDto>>(list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving EvaluationCriterias for task id {taskId}");
                throw;
            }
        }

        public async Task<EvaluationCriteriaDto> CreateAsync(EvaluationCriteriaDto dto)
        {
            try
            {
                // 🎬 Vidéo démonstration (si interne)
                if (!dto.UseExternalDemonstrationVideo && dto.DemonstrationVideoFile != null)
                {
                    var videoName = Guid.NewGuid() + Path.GetExtension(dto.DemonstrationVideoFile.FileName);
                    var videoPath = Path.Combine("uploads", "videos", videoName);
                    var fullPath = Path.Combine(_env.WebRootPath, videoPath);
                    Directory.CreateDirectory(Path.GetDirectoryName(fullPath)!);
                    using var stream = new FileStream(fullPath, FileMode.Create);
                    await dto.DemonstrationVideoFile.CopyToAsync(stream);
                    dto.DemonstrationVideoPath = "/" + videoPath.Replace("\\", "/");
                }

                // 🖼️ Miniature
                if (dto.DemonstrationThumbnail != null)
                {
                    var thumbName = Guid.NewGuid() + Path.GetExtension(dto.DemonstrationThumbnail.FileName);
                    var thumbPath = Path.Combine("uploads", "videos", "thumbnails", thumbName);
                    var fullPath = Path.Combine(_env.WebRootPath, thumbPath);
                    Directory.CreateDirectory(Path.GetDirectoryName(fullPath)!);
                    using var stream = new FileStream(fullPath, FileMode.Create);
                    await dto.DemonstrationThumbnail.CopyToAsync(stream);
                    dto.DemonstrationThumbnailUrl = "/" + thumbPath.Replace("\\", "/");
                }

                // 🎯 Mapping
                var entity = _mapper.Map<EvaluationCriteria>(dto);
                await _unitOfWork.EvaluationCriteriaRepository.CreateAsync(entity);

                // 🧾 On commit d'abord pour obtenir entity.Id correct
                await _unitOfWork.CommitAsync();

                // 📎 Association MaterialPhotoIds
                if (dto.MaterialPhotoIds != null && dto.MaterialPhotoIds.Any())
                {
                    _logger.LogInformation("⛓ MaterialPhotoIds liés au critère : {@MaterialPhotoIds}", dto.MaterialPhotoIds);

                    foreach (var materialId in dto.MaterialPhotoIds)
                    {
                        var link = new EvaluationCriteriaMaterial
                        {
                            EvaluationCriteriaId = entity.Id, // ✅ ID réel après commit
                            MaterialPhotoId = materialId
                        };
                        await _unitOfWork.EvaluationCriteriaMaterialRepository.CreateAsync(link);
                    }

                    await _unitOfWork.CommitAsync(); // commit des liaisons
                }
                else
                {
                    _logger.LogWarning("⚠️ Aucun MaterialPhotoId fourni pour le critère avec label : {Label}", dto.Label);
                }

                return _mapper.Map<EvaluationCriteriaDto>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Erreur lors de la création de EvaluationCriteria");
                throw;
            }
        }




        public async Task<EvaluationCriteriaDto> UpdateAsync(EvaluationCriteriaDto dto)
        {
            try
            {
                // 🎬 Gérer la vidéo démonstration interne si upload
                if (!dto.UseExternalDemonstrationVideo && dto.DemonstrationVideoFile != null)
                {
                    var videoName = Guid.NewGuid() + Path.GetExtension(dto.DemonstrationVideoFile.FileName);
                    var videoPath = Path.Combine("uploads", "videos", videoName);
                    var fullPath = Path.Combine(_env.WebRootPath, videoPath);
                    Directory.CreateDirectory(Path.GetDirectoryName(fullPath)!);
                    using var stream = new FileStream(fullPath, FileMode.Create);
                    await dto.DemonstrationVideoFile.CopyToAsync(stream);
                    dto.DemonstrationVideoPath = "/" + videoPath.Replace("\\", "/");
                }

                // 🖼️ Gérer la miniature
                if (dto.DemonstrationThumbnail != null)
                {
                    var thumbName = Guid.NewGuid() + Path.GetExtension(dto.DemonstrationThumbnail.FileName);
                    var thumbPath = Path.Combine("uploads", "videos", "thumbnails", thumbName);
                    var fullPath = Path.Combine(_env.WebRootPath, thumbPath);
                    Directory.CreateDirectory(Path.GetDirectoryName(fullPath)!);
                    using var stream = new FileStream(fullPath, FileMode.Create);
                    await dto.DemonstrationThumbnail.CopyToAsync(stream);
                    dto.DemonstrationThumbnailUrl = "/" + thumbPath.Replace("\\", "/");
                }

                // 🧠 Mapping principal
                var entity = await _unitOfWork.EvaluationCriteriaRepository.CreateAsync(_mapper.Map<EvaluationCriteria>(dto));

                // 📎 Créer les liens une fois qu'on a un ID valide
                if (dto.MaterialPhotoIds != null && dto.MaterialPhotoIds.Any())
                {
                    foreach (var materialId in dto.MaterialPhotoIds)
                    {
                        var link = new EvaluationCriteriaMaterial
                        {
                            EvaluationCriteriaId = entity.Id, // maintenant OK
                            MaterialPhotoId = materialId
                        };
                        await _unitOfWork.EvaluationCriteriaMaterialRepository.CreateAsync(link);
                    }

                    await _unitOfWork.CommitAsync(); // commit des liaisons
                }


                return _mapper.Map<EvaluationCriteriaDto>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating EvaluationCriteria with id {dto.Id}");
                throw;
            }
        }


        public async Task DeleteAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Deleting EvaluationCriteria with id {id}");
                await _unitOfWork.EvaluationCriteriaRepository.DeleteAsync(id);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting EvaluationCriteria with id {id}");
                throw;
            }
        }
    }
}
