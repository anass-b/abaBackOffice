using AutoMapper;
using abaBackOffice.DTOs;
using abaBackOffice.Interfaces;
using abaBackOffice.Interfaces.Services;
using abaBackOffice.Models;
using Microsoft.Extensions.Logging;

namespace abaBackOffice.Services
{
    public class VideoService : IVideoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<VideoService> _logger;
        private readonly IMapper _mapper;

        public VideoService(IUnitOfWork unitOfWork, ILogger<VideoService> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IEnumerable<VideoDto>> GetAllAsync()
        {
            try
            {
                var videos = await _unitOfWork.VideoRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<VideoDto>>(videos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving videos");
                throw;
            }
        }

        public async Task<VideoDto> GetByIdAsync(int id)
        {
            try
            {
                var video = await _unitOfWork.VideoRepository.GetByIdAsync(id);
                return _mapper.Map<VideoDto>(video);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving video with id {id}");
                throw;
            }
        }

        public async Task<VideoDto> CreateAsync(VideoDto dto)
        {
            try
            {
                // 📁 Gérer l'upload de la miniature
                if (dto.Thumbnail != null && dto.Thumbnail.Length > 0)
                {
                    var thumbnailFolder = Path.Combine("wwwroot", "uploads", "videos", "thumbnails");
                    if (!Directory.Exists(thumbnailFolder))
                        Directory.CreateDirectory(thumbnailFolder);

                    var thumbnailFileName = Guid.NewGuid() + Path.GetExtension(dto.Thumbnail.FileName);
                    var thumbnailPath = Path.Combine(thumbnailFolder, thumbnailFileName);

                    using (var stream = new FileStream(thumbnailPath, FileMode.Create))
                    {
                        await dto.Thumbnail.CopyToAsync(stream);
                    }

                    dto.ThumbnailUrl = $"/uploads/videos/thumbnails/{thumbnailFileName}";
                }

                // 📦 Si ce n’est pas une vidéo externe, gérer le fichier vidéo
                if (!dto.UseExternal && dto.File != null && dto.File.Length > 0)
                {
                    var videoFolder = Path.Combine("wwwroot", "uploads", "videos");
                    if (!Directory.Exists(videoFolder))
                        Directory.CreateDirectory(videoFolder);

                    var videoFileName = Guid.NewGuid() + Path.GetExtension(dto.File.FileName);
                    var videoPath = Path.Combine(videoFolder, videoFileName);

                    using (var stream = new FileStream(videoPath, FileMode.Create))
                    {
                        await dto.File.CopyToAsync(stream);
                    }

                    // 🔗 Enregistrer le lien local comme URL
                    dto.Url = $"/uploads/videos/{videoFileName}";
                }

                // 💾 Mapper, sauvegarder
                var entity = _mapper.Map<Video>(dto);
                await _unitOfWork.VideoRepository.CreateAsync(entity);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<VideoDto>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating video");
                throw;
            }
        }



        public async Task<VideoDto> UpdateAsync(VideoDto dto)
        {
            try
            {
                var entity = _mapper.Map<Video>(dto);
                await _unitOfWork.VideoRepository.UpdateAsync(entity);
                await _unitOfWork.CommitAsync();
                return _mapper.Map<VideoDto>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating video with id {dto.Id}");
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Deleting video with id {id} from database");
                await _unitOfWork.VideoRepository.DeleteAsync(id);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting video with id {id} from database");
                throw;
            }
        }

    }
}