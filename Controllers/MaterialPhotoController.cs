using abaBackOffice.DTOs;
using abaBackOffice.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace abaBackOffice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MaterialPhotoController : ControllerBase
    {
        private readonly IMaterialPhotoService _service;
        private readonly ILogger<MaterialPhotoController> _logger;
        private readonly string _uploadFolder = Path.Combine("wwwroot", "uploads", "materialFiles");

        public MaterialPhotoController(IMaterialPhotoService service, ILogger<MaterialPhotoController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MaterialPhotoDto>>> GetAll()
        {
            _logger.LogInformation("Retrieving all MaterialPhotos");
            var items = await _service.GetAllAsync();
            return Ok(items);
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromForm] MaterialPhotoDto dto)
        {
            _logger.LogInformation("Uploading MaterialPhoto file");

            if (dto.File == null || dto.File.Length == 0)
                return BadRequest("Aucun fichier fourni.");

            try
            {
                // 📁 Créer dossier s'il n'existe pas
                if (!Directory.Exists(_uploadFolder))
                    Directory.CreateDirectory(_uploadFolder);

                // 🧾 Nom unique
                var fileName = Guid.NewGuid() + Path.GetExtension(dto.File.FileName);
                var fullPath = Path.Combine(_uploadFolder, fileName);

                // 💾 Enregistrer fichier
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await dto.File.CopyToAsync(stream);
                }

                // 🎯 Sauvegarder dans la DB
                var fileUrl = $"/uploads/materialFiles/{fileName}";
                var result = await _service.CreateAsync(new MaterialPhotoDto
                {
                    AbllsTaskId = dto.AbllsTaskId,
                    Name = Path.GetFileNameWithoutExtension(dto.File.FileName), // ✅ obligatoire
                    Description = dto.Description,
                    FileUrl = fileUrl,
                    VideoUrl = dto.VideoUrl,
                    Created_by = dto.Created_by,
                    Updated_by = dto.Updated_by
                });

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de l'upload de fichier matériel");
                return StatusCode(500, "Erreur serveur pendant l'upload");
            }
        }




        [HttpGet("{id}")]
        public async Task<ActionResult<MaterialPhotoDto>> GetById(int id)
        {
            _logger.LogInformation($"Retrieving MaterialPhoto with id {id}");
            try
            {
                var item = await _service.GetByIdAsync(id);
                return Ok(item);
            }
            catch (KeyNotFoundException)
            {
                _logger.LogWarning($"MaterialPhoto with id {id} not found");
                return NotFound();
            }
        }

        [HttpGet("task/{taskId}")]
        public async Task<ActionResult<IEnumerable<MaterialPhotoDto>>> GetByTaskId(int taskId)
        {
            _logger.LogInformation($"Retrieving MaterialPhotos for task id {taskId}");
            var items = await _service.GetByTaskIdAsync(taskId);
            return Ok(items);
        }

        [HttpPost]
        public async Task<ActionResult<MaterialPhotoDto>> Create(MaterialPhotoDto dto)
        {
            _logger.LogInformation("Creating MaterialPhoto");
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, MaterialPhotoDto dto)
        {
            if (id != dto.Id)
            {
                _logger.LogWarning("MaterialPhoto ID mismatch");
                return BadRequest();
            }

            _logger.LogInformation($"Updating MaterialPhoto with id {id}");
            var updated = await _service.UpdateAsync(dto);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation($"Deleting MaterialPhoto with id {id}");
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
