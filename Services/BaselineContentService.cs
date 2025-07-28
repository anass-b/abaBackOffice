using AutoMapper;
using abaBackOffice.DTOs;
using abaBackOffice.Interfaces;
using abaBackOffice.Interfaces.Services;
using abaBackOffice.Models;
using Microsoft.Extensions.Logging;

namespace abaBackOffice.Services
{
    public class BaselineContentService : IBaselineContentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<BaselineContentService> _logger;
        private readonly IMapper _mapper;

        public BaselineContentService(IUnitOfWork unitOfWork, ILogger<BaselineContentService> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BaselineContentDto>> GetAllAsync()
        {
            try
            {
                var entities = await _unitOfWork.BaselineContentRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<BaselineContentDto>>(entities);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving BaselineContents");
                throw;
            }
        }

        public async Task<BaselineContentDto> GetByIdAsync(int id)
        {
            try
            {
                var entity = await _unitOfWork.BaselineContentRepository.GetByIdAsync(id);
                if (entity == null)
                    throw new KeyNotFoundException($"BaselineContent with ID {id} not found.");

                return _mapper.Map<BaselineContentDto>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving BaselineContent with id {id}");
                throw;
            }
        }

        public async Task<IEnumerable<BaselineContentDto>> GetByTaskIdAsync(int taskId)
        {
            try
            {
                var list = await _unitOfWork.BaselineContentRepository.GetByTaskIdAsync(taskId);
                return _mapper.Map<IEnumerable<BaselineContentDto>>(list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving BaselineContents for task id {taskId}");
                throw;
            }
        }

        public async Task<BaselineContentDto> CreateAsync(BaselineContentDto dto)
        {
            try
            {
                var entity = _mapper.Map<BaselineContent>(dto);
                await _unitOfWork.BaselineContentRepository.CreateAsync(entity);
                await _unitOfWork.CommitAsync();
                return _mapper.Map<BaselineContentDto>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating BaselineContent");
                throw;
            }
        }

        public async Task<BaselineContentDto> UpdateAsync(BaselineContentDto dto)
        {
            try
            {
                var entity = _mapper.Map<BaselineContent>(dto);
                await _unitOfWork.BaselineContentRepository.UpdateAsync(entity);
                await _unitOfWork.CommitAsync();
                return _mapper.Map<BaselineContentDto>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating BaselineContent with id {dto.Id}");
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Deleting BaselineContent with id {id} from database");
                await _unitOfWork.BaselineContentRepository.DeleteAsync(id);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting BaselineContent with id {id} from database");
                throw;
            }
        }
    }
}
