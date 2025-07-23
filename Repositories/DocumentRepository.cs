// -------------------- DocumentRepository --------------------
using abaBackOffice.DataAccessLayer;
using abaBackOffice.Interfaces.Repositories;
using abaBackOffice.Models;
using Microsoft.EntityFrameworkCore;

namespace abaBackOffice.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly AbaDbContext _context;
        private readonly ILogger<DocumentRepository> _logger;

        public DocumentRepository(AbaDbContext context, ILogger<DocumentRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Document>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving all documents from database");
                return await _context.Documents
                    .AsNoTracking()
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving documents from database");
                throw;
            }
        }

        public async Task<Document> GetByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Retrieving document with id {id} from database");
                return await _context.Documents.FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving document with id {id} from database");
                throw;
            }
        }

        public async Task CreateAsync(Document document)
        {
            try
            {
                _logger.LogInformation("Creating new document in database");
                await _context.Documents.AddAsync(document);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating document in database");
                throw;
            }
        }

        public async Task UpdateAsync(Document document)
        {
            try
            {
                _logger.LogInformation($"Updating document with id {document.Id} in database");
                _context.Documents.Update(document);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating document with id {document.Id} in database");
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Deleting document with id {id} from database");
                var document = await _context.Documents.FindAsync(id);
                if (document != null)
                {
                    _context.Documents.Remove(document);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting document with id {id} from database");
                throw;
            }
        }

    }
}
