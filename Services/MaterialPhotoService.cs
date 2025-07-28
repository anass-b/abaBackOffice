using AutoMapper;
using abaBackOffice.DTOs;
using abaBackOffice.Interfaces;
using abaBackOffice.Interfaces.Services;
using abaBackOffice.Models;
using Microsoft.Extensions.Logging;

namespace abaBackOffice.Services
{
    public class MaterialPhotoService : IMaterialPhotoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<MaterialPhotoService> _logger;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public MaterialPhotoService(IUnitOfWork unitOfWork, ILogger<MaterialPhotoService> logger, IMapper mapper, IWebHostEnvironment env)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            _env = env;
        }

        public async Task<IEnumerable<MaterialPhotoDto>> GetAllAsync()
        {
            try
            {
                var entities = await _unitOfWork.MaterialPhotoRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<MaterialPhotoDto>>(entities);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving MaterialPhotos");
                throw;
            }
        }

        public async Task<MaterialPhotoDto> GetByIdAsync(int id)
        {
            try
            {
                var entity = await _unitOfWork.MaterialPhotoRepository.GetByIdAsync(id);
                if (entity == null)
                    throw new KeyNotFoundException($"MaterialPhoto with ID {id} not found.");
                return _mapper.Map<MaterialPhotoDto>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving MaterialPhoto with id {id}");
                throw;
            }
        }

        public async Task<IEnumerable<MaterialPhotoDto>> GetByTaskIdAsync(int taskId)
        {
            try
            {
                var list = await _unitOfWork.MaterialPhotoRepository.GetByTaskIdAsync(taskId);
                return _mapper.Map<IEnumerable<MaterialPhotoDto>>(list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving MaterialPhotos for task id {taskId}");
                throw;
            }
        }

        public async Task<MaterialPhotoDto> CreateAsync(MaterialPhotoDto dto)
        {
            try
            {
                // 📁 Gérer le fichier (image, pdf, etc.)
                if (dto.File != null && dto.File.Length > 0)
                {
                    var uniqueName = Guid.NewGuid() + Path.GetExtension(dto.File.FileName);
                    var relPath = Path.Combine("uploads", "materials", uniqueName);
                    var fullPath = Path.Combine(_env.WebRootPath, relPath);

                    Directory.CreateDirectory(Path.GetDirectoryName(fullPath)!);
                    using var stream = new FileStream(fullPath, FileMode.Create);
                    await dto.File.CopyToAsync(stream);

                    dto.FileUrl = "/" + relPath.Replace("\\", "/");
                }

                var entity = _mapper.Map<MaterialPhoto>(dto);
                await _unitOfWork.MaterialPhotoRepository.CreateAsync(entity);
                await _unitOfWork.CommitAsync();
                return _mapper.Map<MaterialPhotoDto>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating MaterialPhoto");
                throw;
            }
        }


        public async Task<MaterialPhotoDto> UpdateAsync(MaterialPhotoDto dto)
        {
            try
            {
                var entity = _mapper.Map<MaterialPhoto>(dto);
                await _unitOfWork.MaterialPhotoRepository.UpdateAsync(entity);
                await _unitOfWork.CommitAsync();
                return _mapper.Map<MaterialPhotoDto>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating MaterialPhoto with id {dto.Id}");
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Deleting MaterialPhoto with id {id}");
                await _unitOfWork.MaterialPhotoRepository.DeleteAsync(id);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting MaterialPhoto with id {id}");
                throw;
            }
        }
    }
}
