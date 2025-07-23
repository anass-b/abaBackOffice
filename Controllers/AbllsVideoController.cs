using abaBackOffice.Interfaces.Services;
using abaBackOffice.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace abaBackOffice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AbllsVideoController : ControllerBase
    {
        private readonly IAbllsVideoService _abllsVideoService;
        private readonly ILogger<AbllsVideoController> _logger;

        public AbllsVideoController(IAbllsVideoService abllsVideoService, ILogger<AbllsVideoController> logger)
        {
            _abllsVideoService = abllsVideoService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AbllsVideoDto>>> GetAllVideos()
        {
            _logger.LogInformation("Retrieving all ABLLS videos");
            var videos = await _abllsVideoService.GetAllAsync();
            return Ok(videos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AbllsVideoDto>> GetVideoById(int id)
        {
            _logger.LogInformation($"Retrieving ABLLS video with id {id}");
            var video = await _abllsVideoService.GetByIdAsync(id);
            if (video == null)
            {
                _logger.LogWarning($"ABLLS video with id {id} not found");
                return NotFound();
            }
            return Ok(video);
        }

        [HttpPost]
        public async Task<ActionResult<AbllsVideoDto>> CreateVideo(AbllsVideoDto abllsVideoDto)
        {
            _logger.LogInformation("Creating a new ABLLS video");
            var createdVideo = await _abllsVideoService.CreateAsync(abllsVideoDto);
            return CreatedAtAction(nameof(GetVideoById), new { id = createdVideo.Id }, createdVideo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVideo(int id, AbllsVideoDto abllsVideoDto)
        {
            if (id != abllsVideoDto.Id)
            {
                _logger.LogWarning("ABLLS video ID mismatch");
                return BadRequest();
            }

            _logger.LogInformation($"Updating ABLLS video with id {id}");
            var updatedVideo = await _abllsVideoService.UpdateAsync(abllsVideoDto);
            return Ok(updatedVideo);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVideo(int id)
        {
            _logger.LogInformation($"Deleting ABLLS video with id {id}");
            await _abllsVideoService.DeleteAsync(id);
            return NoContent();
        }
    }
}
