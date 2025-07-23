// -------------------- AbllsVideoService --------------------
using AutoMapper;
using abaBackOffice.DTOs;
using abaBackOffice.Interfaces;
using abaBackOffice.Interfaces.Services;
using abaBackOffice.Models;
using Microsoft.Extensions.Logging;

namespace abaBackOffice.Services
{
    public class AbllsVideoService : IAbllsVideoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AbllsVideoService> _logger;
        private readonly IMapper _mapper;

        public AbllsVideoService(IUnitOfWork unitOfWork, ILogger<AbllsVideoService> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AbllsVideoDto>> GetAllAsync()
        {
            try
            {
                var videos = await _unitOfWork.AbllsVideoRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<AbllsVideoDto>>(videos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving ABLLS videos");
                throw;
            }
        }

        public async Task<AbllsVideoDto> GetByIdAsync(int id)
        {
            try
            {
                var video = await _unitOfWork.AbllsVideoRepository.GetByIdAsync(id);
                return _mapper.Map<AbllsVideoDto>(video);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving ABLLS video with id {id}");
                throw;
            }
        }

        public async Task<AbllsVideoDto> CreateAsync(AbllsVideoDto dto)
        {
            try
            {
                var entity = _mapper.Map<AbllsVideo>(dto);
                await _unitOfWork.AbllsVideoRepository.CreateAsync(entity);
                await _unitOfWork.CommitAsync();
                return _mapper.Map<AbllsVideoDto>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating ABLLS video");
                throw;
            }
        }

        public async Task<AbllsVideoDto> UpdateAsync(AbllsVideoDto dto)
        {
            try
            {
                var entity = _mapper.Map<AbllsVideo>(dto);
                await _unitOfWork.AbllsVideoRepository.UpdateAsync(entity);
                await _unitOfWork.CommitAsync();
                return _mapper.Map<AbllsVideoDto>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating ABLLS video with id {dto.Id}");
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Deleting ABLLS video with id {id} from database");
                await _unitOfWork.AbllsVideoRepository.DeleteAsync(id);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting ABLLS video with id {id} from database");
                throw;
            }
        }

    }
}
