// -------------------- ReinforcerAgentService --------------------
using AutoMapper;
using abaBackOffice.DTOs;
using abaBackOffice.Interfaces;
using abaBackOffice.Interfaces.Services;
using abaBackOffice.Models;
using Microsoft.Extensions.Logging;

namespace abaBackOffice.Services
{
    public class ReinforcerAgentService : IReinforcerAgentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ReinforcerAgentService> _logger;
        private readonly IMapper _mapper;

        public ReinforcerAgentService(IUnitOfWork unitOfWork, ILogger<ReinforcerAgentService> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReinforcerAgentDto>> GetAllAsync()
        {
            try
            {
                var agents = await _unitOfWork.ReinforcerAgentRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<ReinforcerAgentDto>>(agents);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving reinforcer agents");
                throw;
            }
        }

        public async Task<ReinforcerAgentDto> GetByIdAsync(int id)
        {
            try
            {
                var agent = await _unitOfWork.ReinforcerAgentRepository.GetByIdAsync(id);
                return _mapper.Map<ReinforcerAgentDto>(agent);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving reinforcer agent with id {id}");
                throw;
            }
        }

        public async Task<ReinforcerAgentDto> CreateAsync(ReinforcerAgentDto dto)
        {
            try
            {
                var entity = _mapper.Map<ReinforcerAgent>(dto);
                await _unitOfWork.ReinforcerAgentRepository.CreateAsync(entity);
                await _unitOfWork.CommitAsync();
                return _mapper.Map<ReinforcerAgentDto>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating reinforcer agent");
                throw;
            }
        }

        public async Task<ReinforcerAgentDto> UpdateAsync(ReinforcerAgentDto dto)
        {
            try
            {
                var entity = _mapper.Map<ReinforcerAgent>(dto);
                await _unitOfWork.ReinforcerAgentRepository.UpdateAsync(entity);
                await _unitOfWork.CommitAsync();
                return _mapper.Map<ReinforcerAgentDto>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating reinforcer agent with id {dto.Id}");
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Deleting reinforcer agent with id {id} from database");
                await _unitOfWork.ReinforcerAgentRepository.DeleteAsync(id);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting reinforcer agent with id {id} from database");
                throw;
            }
        }

    }
}
