using abaBackOffice.Interfaces.Services;
using abaBackOffice.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace abaBackOffice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionService _subscriptionService;
        private readonly ILogger<SubscriptionController> _logger;

        public SubscriptionController(ISubscriptionService subscriptionService, ILogger<SubscriptionController> logger)
        {
            _subscriptionService = subscriptionService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubscriptionDto>>> GetAllSubscriptions()
        {
            _logger.LogInformation("Retrieving all subscriptions");
            var subscriptions = await _subscriptionService.GetAllAsync();
            return Ok(subscriptions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SubscriptionDto>> GetSubscriptionById(int id)
        {
            _logger.LogInformation($"Retrieving subscription with id {id}");
            var subscription = await _subscriptionService.GetByIdAsync(id);
            if (subscription == null)
            {
                _logger.LogWarning($"Subscription with id {id} not found");
                return NotFound();
            }
            return Ok(subscription);
        }

        [HttpPost]
        public async Task<ActionResult<SubscriptionDto>> CreateSubscription(SubscriptionDto subscriptionDto)
        {
            _logger.LogInformation("Creating a new subscription");
            var createdSubscription = await _subscriptionService.CreateAsync(subscriptionDto);
            return CreatedAtAction(nameof(GetSubscriptionById), new { id = createdSubscription.Id }, createdSubscription);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSubscription(int id, SubscriptionDto subscriptionDto)
        {
            if (id != subscriptionDto.Id)
            {
                _logger.LogWarning("Subscription ID mismatch");
                return BadRequest();
            }

            _logger.LogInformation($"Updating subscription with id {id}");
            var updatedSubscription = await _subscriptionService.UpdateAsync(subscriptionDto);
            return Ok(updatedSubscription);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubscription(int id)
        {
            _logger.LogInformation($"Deleting subscription with id {id}");
            await _subscriptionService.DeleteAsync(id);
            return NoContent();
        }
    }
}
