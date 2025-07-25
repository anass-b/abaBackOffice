// -------------------- SubscriptionService --------------------
using AutoMapper;
using abaBackOffice.DTOs;
using abaBackOffice.Interfaces;
using abaBackOffice.Interfaces.Services;
using abaBackOffice.Models;
using Microsoft.Extensions.Logging;

namespace abaBackOffice.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<SubscriptionService> _logger;
        private readonly IMapper _mapper;

        public SubscriptionService(IUnitOfWork unitOfWork, ILogger<SubscriptionService> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SubscriptionDto>> GetAllAsync()
        {
            try
            {
                var entities = await _unitOfWork.SubscriptionRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<SubscriptionDto>>(entities);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving subscriptions");
                throw;
            }
        }

        public async Task<SubscriptionDto> GetByIdAsync(int id)
        {
            try
            {
                var entity = await _unitOfWork.SubscriptionRepository.GetByIdAsync(id);
                return _mapper.Map<SubscriptionDto>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving subscription with id {id}");
                throw;
            }
        }
        public async Task<IEnumerable<SubscriptionDto>> GetByUserIdAsync(int userId)
        {
            try
            {
                var subs = await _unitOfWork.SubscriptionRepository.GetByIdAsync(userId);
                return _mapper.Map<IEnumerable<SubscriptionDto>>(subs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving subscription with user id {userId}");
                throw;
            }
            
        }


        public async Task<SubscriptionDto> CreateAsync(SubscriptionDto dto)
        {
            try
            {
                var entity = _mapper.Map<Subscription>(dto);
                await _unitOfWork.SubscriptionRepository.CreateAsync(entity);
                await _unitOfWork.CommitAsync();
                return _mapper.Map<SubscriptionDto>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating subscription");
                throw;
            }
        }

        public async Task<SubscriptionDto> UpdateAsync(SubscriptionDto dto)
        {
            try
            {
                var entity = _mapper.Map<Subscription>(dto);
                await _unitOfWork.SubscriptionRepository.UpdateAsync(entity);
                await _unitOfWork.CommitAsync();
                return _mapper.Map<SubscriptionDto>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating subscription with id {dto.Id}");
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Deleting subscription with id {id} from database");
                await _unitOfWork.SubscriptionRepository.DeleteAsync(id);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting subscription with id {id} from database");
                throw;
            }
        }

    }
}
