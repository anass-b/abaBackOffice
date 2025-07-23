// -------------------- AbllsTaskService --------------------
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

        public AbllsTaskService(IUnitOfWork unitOfWork, ILogger<AbllsTaskService> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
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
                var entity = _mapper.Map<AbllsTask>(dto);
                await _unitOfWork.AbllsTaskRepository.CreateAsync(entity);
                await _unitOfWork.CommitAsync();
                return _mapper.Map<AbllsTaskDto>(entity);
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
