using abaBackOffice.DTOs;
using abaBackOffice.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace abaBackOffice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VideoController : ControllerBase
    {
        private readonly IVideoService _videoService;
        private readonly ILogger<VideoController> _logger;

        public VideoController(IVideoService videoService, ILogger<VideoController> logger)
        {
            _videoService = videoService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VideoDto>>> GetAllVideos()
        {
            _logger.LogInformation("Retrieving all videos");
            var videos = await _videoService.GetAllAsync();
            return Ok(videos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VideoDto>> GetVideoById(int id)
        {
            _logger.LogInformation($"Retrieving video with id {id}");
            var video = await _videoService.GetByIdAsync(id);
            if (video == null)
            {
                _logger.LogWarning($"Video with id {id} not found");
                return NotFound();
            }
            return Ok(video);
        }
        [HttpGet("stream/{id}")]
        public async Task<IActionResult> StreamVideo(int id)
        {
            _logger.LogInformation($"Appel stream vidéo pour ID {id}");
            var video = await _videoService.GetByIdAsync(id);
            if (video == null || string.IsNullOrWhiteSpace(video.Url))
                return NotFound("Vidéo introuvable ou URL invalide.");

            var relativePath = video.Url.Trim().TrimStart('/');
            var videoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", relativePath);

            if (!System.IO.File.Exists(videoPath))
            {
                _logger.LogWarning($"Fichier vidéo non trouvé : {videoPath}");
                return NotFound("Fichier non trouvé sur le serveur.");
            }

            try
            {
                var stream = new FileStream(videoPath, FileMode.Open, FileAccess.Read);

                // Détecter le type MIME selon l'extension
                var ext = Path.GetExtension(videoPath).ToLowerInvariant();
                var mime = ext switch
                {
                    ".mp4" => "video/mp4",
                    ".mov" => "video/quicktime",
                    ".avi" => "video/x-msvideo",
                    _ => "application/octet-stream"
                };

                return File(stream, mime, enableRangeProcessing: true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de l'ouverture du fichier vidéo");
                return StatusCode(500, "Erreur lors de la lecture du fichier.");
            }
        }




        [HttpPost]
        public async Task<ActionResult<VideoDto>> CreateVideo([FromForm] VideoDto videoDto)
        {
            _logger.LogInformation("Creating a new video");

            var createdVideo = await _videoService.CreateAsync(videoDto);

            return CreatedAtAction(nameof(GetVideoById), new { id = createdVideo.Id }, createdVideo);
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVideo(int id, VideoDto videoDto)
        {
            if (id != videoDto.Id)
            {
                _logger.LogWarning("Video ID mismatch");
                return BadRequest();
            }

            _logger.LogInformation($"Updating video with id {id}");
            var updatedVideo = await _videoService.UpdateAsync(videoDto);
            return Ok(updatedVideo);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVideo(int id)
        {
            _logger.LogInformation($"Deleting video with id {id}");
            await _videoService.DeleteAsync(id);
            return NoContent();
        }
    }
}
