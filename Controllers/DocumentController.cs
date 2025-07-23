using abaBackOffice.Interfaces.Services;
using abaBackOffice.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace abaBackOffice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService _documentService;
        private readonly ILogger<DocumentController> _logger;

        public DocumentController(IDocumentService documentService, ILogger<DocumentController> logger)
        {
            _documentService = documentService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DocumentDto>>> GetAllDocuments()
        {
            _logger.LogInformation("Retrieving all documents");
            var documents = await _documentService.GetAllAsync();
            return Ok(documents);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DocumentDto>> GetDocumentById(int id)
        {
            _logger.LogInformation($"Retrieving document with id {id}");
            var document = await _documentService.GetByIdAsync(id);
            if (document == null)
            {
                _logger.LogWarning($"Document with id {id} not found");
                return NotFound();
            }
            return Ok(document);
        }

        [HttpPost]
        public async Task<ActionResult<DocumentDto>> CreateDocument([FromForm] DocumentDto documentDto)
        {
            _logger.LogInformation("Creating a new document (multipart/form-data)");
            var createdDocument = await _documentService.CreateAsync(documentDto);
            return CreatedAtAction(nameof(GetDocumentById), new { id = createdDocument.Id }, createdDocument);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDocument(int id, DocumentDto documentDto)
        {
            if (id != documentDto.Id)
            {
                _logger.LogWarning("Document ID mismatch");
                return BadRequest();
            }

            _logger.LogInformation($"Updating document with id {id}");
            var updatedDocument = await _documentService.UpdateAsync(documentDto);
            return Ok(updatedDocument);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocument(int id)
        {
            _logger.LogInformation($"Deleting document with id {id}");
            await _documentService.DeleteAsync(id);
            return NoContent();
        }
    }
}
