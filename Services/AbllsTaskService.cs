using AutoMapper;
using abaBackOffice.DTOs;
using abaBackOffice.Interfaces;
using abaBackOffice.Interfaces.Services;
using abaBackOffice.Models;
using Microsoft.Extensions.Logging;

namespace abaBackOffice.Services
{
    public class AbllsTaskService : IAbllsTaskService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AbllsTaskService> _logger;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public AbllsTaskService(IUnitOfWork unitOfWork, ILogger<AbllsTaskService> logger, IMapper mapper, IWebHostEnvironment env)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            _env = env;
        }

        public async Task<IEnumerable<AbllsTaskDto>> GetAllAsync()
        {
            try
            {
                var tasks = await _unitOfWork.AbllsTaskRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<AbllsTaskDto>>(tasks);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving ABLLS tasks");
                throw;
            }
        }

        public async Task<AbllsTaskDto> GetByIdAsync(int id)
        {
            try
            {
                var task = await _unitOfWork.AbllsTaskRepository.GetByIdAsync(id);
                if (task == null)
                    throw new KeyNotFoundException($"ABLLS Task with ID {id} not found.");

                return _mapper.Map<AbllsTaskDto>(task);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving ABLLS task with id {id}");
                throw;
            }
        }

        public async Task<AbllsTaskDto> CreateAsync(AbllsTaskDto dto)
        {
            try
            {
                _logger.LogInformation("Creating new ABLLS task");

                // 🎬 Vidéo explicative
                if (!dto.UseExternalExplanationVideo && dto.ExplanationVideoFile != null)
                {
                    var videoName = Guid.NewGuid() + Path.GetExtension(dto.ExplanationVideoFile.FileName);
                    var videoPath = Path.Combine("uploads", "videos", videoName);
                    var fullVideoPath = Path.Combine(_env.WebRootPath, videoPath);
                    Directory.CreateDirectory(Path.GetDirectoryName(fullVideoPath)!);
                    using var stream = new FileStream(fullVideoPath, FileMode.Create);
                    await dto.ExplanationVideoFile.CopyToAsync(stream);
                    dto.ExplanationVideoPath = "/" + videoPath.Replace("\\", "/");
                }

                if (dto.ExplanationThumbnail != null)
                {
                    var thumbName = Guid.NewGuid() + Path.GetExtension(dto.ExplanationThumbnail.FileName);
                    var thumbPath = Path.Combine("uploads", "videos", "thumbnails", thumbName);
                    var fullThumbPath = Path.Combine(_env.WebRootPath, thumbPath);
                    Directory.CreateDirectory(Path.GetDirectoryName(fullThumbPath)!);
                    using var stream = new FileStream(fullThumbPath, FileMode.Create);
                    await dto.ExplanationThumbnail.CopyToAsync(stream);
                    dto.ExplanationThumbnailUrl = "/" + thumbPath.Replace("\\", "/");
                }

                var taskEntity = _mapper.Map<AbllsTask>(dto);

                // 🧪 Critères
                if (dto.EvaluationCriterias != null)
                {
                    taskEntity.EvaluationCriterias = new List<EvaluationCriteria>();
                    foreach (var crit in dto.EvaluationCriterias)
                    {
                        if (!crit.UseExternalDemonstrationVideo && crit.DemonstrationVideoFile != null)
                        {
                            var videoName = Guid.NewGuid() + Path.GetExtension(crit.DemonstrationVideoFile.FileName);
                            var videoPath = Path.Combine("uploads", "videos", videoName);
                            var fullPath = Path.Combine(_env.WebRootPath, videoPath);
                            Directory.CreateDirectory(Path.GetDirectoryName(fullPath)!);
                            using var stream = new FileStream(fullPath, FileMode.Create);
                            await crit.DemonstrationVideoFile.CopyToAsync(stream);
                            crit.DemonstrationVideoPath = "/" + videoPath.Replace("\\", "/");
                        }

                        if (crit.DemonstrationThumbnail != null)
                        {
                            var thumbName = Guid.NewGuid() + Path.GetExtension(crit.DemonstrationThumbnail.FileName);
                            var thumbPath = Path.Combine("uploads", "videos", "thumbnails", thumbName);
                            var fullPath = Path.Combine(_env.WebRootPath, thumbPath);
                            Directory.CreateDirectory(Path.GetDirectoryName(fullPath)!);
                            using var stream = new FileStream(fullPath, FileMode.Create);
                            await crit.DemonstrationThumbnail.CopyToAsync(stream);
                            crit.DemonstrationThumbnailUrl = "/" + thumbPath.Replace("\\", "/");
                        }

                        var critEntity = _mapper.Map<EvaluationCriteria>(crit);
                        taskEntity.EvaluationCriterias.Add(critEntity);
                    }
                }

                // 🧰 Matériel
                if (dto.MaterialPhotos != null)
                {
                    taskEntity.MaterialPhotos = new List<MaterialPhoto>();
                    foreach (var mat in dto.MaterialPhotos)
                    {
                        if (mat.File != null)
                        {
                            var fileName = Guid.NewGuid() + Path.GetExtension(mat.File.FileName);
                            var filePath = Path.Combine("uploads", "materials", fileName);
                            var fullPath = Path.Combine(_env.WebRootPath, filePath);
                            Directory.CreateDirectory(Path.GetDirectoryName(fullPath)!);
                            using var stream = new FileStream(fullPath, FileMode.Create);
                            await mat.File.CopyToAsync(stream);
                            mat.FileUrl = "/" + filePath.Replace("\\", "/");
                        }

                        var matEntity = _mapper.Map<MaterialPhoto>(mat);
                        taskEntity.MaterialPhotos.Add(matEntity);
                    }
                }

                // 📊 Ligne de base
                if (dto.BaselineContents != null)
                {
                    taskEntity.BaselineContents = new List<BaselineContent>();
                    foreach (var baseItem in dto.BaselineContents)
                    {
                        if (baseItem.File != null)
                        {
                            var fileName = Guid.NewGuid() + Path.GetExtension(baseItem.File.FileName);
                            var filePath = Path.Combine("uploads", "baseline", fileName);
                            var fullPath = Path.Combine(_env.WebRootPath, filePath);
                            Directory.CreateDirectory(Path.GetDirectoryName(fullPath)!);
                            using var stream = new FileStream(fullPath, FileMode.Create);
                            await baseItem.File.CopyToAsync(stream);
                            baseItem.FileUrl = "/" + filePath.Replace("\\", "/");
                        }

                        var baseEntity = _mapper.Map<BaselineContent>(baseItem);
                        taskEntity.BaselineContents.Add(baseEntity);
                    }
                }

                await _unitOfWork.AbllsTaskRepository.CreateAsync(taskEntity);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<AbllsTaskDto>(taskEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating ABLLS task");
                throw;
            }
        }

        public async Task<AbllsTaskDto> UpdateAsync(AbllsTaskDto dto)
        {
            try
            {
                var entity = _mapper.Map<AbllsTask>(dto);
                await _unitOfWork.AbllsTaskRepository.UpdateAsync(entity);
                await _unitOfWork.CommitAsync();
                return _mapper.Map<AbllsTaskDto>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating ABLLS task with id {dto.Id}");
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Deleting ABLLS task with id {id} from database");
                await _unitOfWork.AbllsTaskRepository.DeleteAsync(id);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting ABLLS task with id {id} from database");
                throw;
            }
        }
    }
}
