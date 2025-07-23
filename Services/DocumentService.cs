// -------------------- DocumentService --------------------
using AutoMapper;
using abaBackOffice.DTOs;
using abaBackOffice.Interfaces;
using abaBackOffice.Interfaces.Services;
using abaBackOffice.Models;
using Microsoft.Extensions.Logging;

namespace abaBackOffice.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DocumentService> _logger;
        private readonly IMapper _mapper;

        public DocumentService(IUnitOfWork unitOfWork, ILogger<DocumentService> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DocumentDto>> GetAllAsync()
        {
            try
            {
                var documents = await _unitOfWork.DocumentRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<DocumentDto>>(documents);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving documents");
                throw;
            }
        }

        public async Task<DocumentDto> GetByIdAsync(int id)
        {
            try
            {
                var document = await _unitOfWork.DocumentRepository.GetByIdAsync(id);
                return _mapper.Map<DocumentDto>(document);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving document with id {id}");
                throw;
            }
        }

        public async Task<DocumentDto> CreateAsync(DocumentDto dto)
        {
            try
            {
                // 📁 1. Gérer le fichier si présent
                if (dto.File != null && dto.File.Length > 0)
                {
                    var uploadsFolder = Path.Combine("wwwroot", "uploads", "documents");

                    if (!Directory.Exists(uploadsFolder))
                        Directory.CreateDirectory(uploadsFolder);

                    var uniqueFileName = Guid.NewGuid() + Path.GetExtension(dto.File.FileName);
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await dto.File.CopyToAsync(stream);
                    }

                    // 🔗 Générer FileUrl relatif
                    dto.FileUrl = $"/uploads/documents/{uniqueFileName}";
                }

                // 📂 2. Stocker le nom des catégories (si envoyées comme tableau JSON)
                if (!string.IsNullOrWhiteSpace(dto.Category))
                {
                    dto.Category = dto.Category.Replace("[", "").Replace("]", "").Replace("\"", "");
                }

                // 💾 3. Sauvegarder en base
                var entity = _mapper.Map<Document>(dto);
                await _unitOfWork.DocumentRepository.CreateAsync(entity);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<DocumentDto>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating document");
                throw;
            }
        }


        public async Task<DocumentDto> UpdateAsync(DocumentDto dto)
        {
            try
            {
                var entity = _mapper.Map<Document>(dto);
                await _unitOfWork.DocumentRepository.UpdateAsync(entity);
                await _unitOfWork.CommitAsync();
                return _mapper.Map<DocumentDto>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating document with id {dto.Id}");
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Deleting document with id {id} from database");
                await _unitOfWork.DocumentRepository.DeleteAsync(id);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting document with id {id} from database");
                throw;
            }
        }

    }
}
