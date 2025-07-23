// -------------------- BlogCommentService --------------------
using AutoMapper;
using abaBackOffice.DTOs;
using abaBackOffice.Interfaces;
using abaBackOffice.Interfaces.Services;
using abaBackOffice.Models;
using Microsoft.Extensions.Logging;

namespace abaBackOffice.Services
{
    public class BlogCommentService : IBlogCommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<BlogCommentService> _logger;
        private readonly IMapper _mapper;

        public BlogCommentService(IUnitOfWork unitOfWork, ILogger<BlogCommentService> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BlogCommentDto>> GetAllAsync()
        {
            try
            {
                var comments = await _unitOfWork.BlogCommentRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<BlogCommentDto>>(comments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving blog comments");
                throw;
            }
        }

        public async Task<BlogCommentDto> GetByIdAsync(int id)
        {
            try
            {
                var comment = await _unitOfWork.BlogCommentRepository.GetByIdAsync(id);
                return _mapper.Map<BlogCommentDto>(comment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving blog comment with id {id}");
                throw;
            }
        }

        public async Task<BlogCommentDto> CreateAsync(BlogCommentDto dto)
        {
            try
            {
                var entity = _mapper.Map<BlogComment>(dto);
                await _unitOfWork.BlogCommentRepository.CreateAsync(entity);
                await _unitOfWork.CommitAsync();
                return _mapper.Map<BlogCommentDto>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating blog comment");
                throw;
            }
        }

        public async Task<BlogCommentDto> UpdateAsync(BlogCommentDto dto)
        {
            try
            {
                var entity = _mapper.Map<BlogComment>(dto);
                await _unitOfWork.BlogCommentRepository.UpdateAsync(entity);
                await _unitOfWork.CommitAsync();
                return _mapper.Map<BlogCommentDto>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating blog comment with id {dto.Id}");
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Deleting blog comment with id {id} from database");
                await _unitOfWork.BlogCommentRepository.DeleteAsync(id);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting blog comment with id {id} from database");
                throw;
            }
        }

    }
}
