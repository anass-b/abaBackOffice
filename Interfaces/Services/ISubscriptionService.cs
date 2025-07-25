using abaBackOffice.DTOs;

namespace abaBackOffice.Interfaces.Services
{
    public interface ISubscriptionService
    {
        Task<IEnumerable<SubscriptionDto>> GetAllAsync();
        Task<SubscriptionDto> GetByIdAsync(int id);
        Task<IEnumerable<SubscriptionDto>> GetByUserIdAsync(int userId);
        Task<SubscriptionDto> CreateAsync(SubscriptionDto subscriptionDto);
        Task<SubscriptionDto> UpdateAsync(SubscriptionDto subscriptionDto);
        Task DeleteAsync(int id);
    }
}