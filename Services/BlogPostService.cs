// -------------------- BlogPostService --------------------
using AutoMapper;
using abaBackOffice.DTOs;
using abaBackOffice.Interfaces;
using abaBackOffice.Interfaces.Services;
using abaBackOffice.Models;
using Microsoft.Extensions.Logging;

namespace abaBackOffice.Services
{
    public class BlogPostService : IBlogPostService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<BlogPostService> _logger;
        private readonly IMapper _mapper;

        public BlogPostService(IUnitOfWork unitOfWork, ILogger<BlogPostService> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BlogPostDto>> GetAllAsync()
        {
            try
            {
                var posts = await _unitOfWork.BlogPostRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<BlogPostDto>>(posts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving blog posts");
                throw;
            }
        }

        public async Task<BlogPostDto> GetByIdAsync(int id)
        {
            try
            {
                var post = await _unitOfWork.BlogPostRepository.GetByIdAsync(id);
                return _mapper.Map<BlogPostDto>(post);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving blog post with id {id}");
                throw;
            }
        }

        public async Task<BlogPostDto> CreateAsync(BlogPostDto dto)
        {
            try
            {
                var entity = _mapper.Map<BlogPost>(dto);
                await _unitOfWork.BlogPostRepository.CreateAsync(entity);
                await _unitOfWork.CommitAsync();
                return _mapper.Map<BlogPostDto>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating blog post");
                throw;
            }
        }

        public async Task<BlogPostDto> UpdateAsync(BlogPostDto dto)
        {
            try
            {
                var entity = _mapper.Map<BlogPost>(dto);
                await _unitOfWork.BlogPostRepository.UpdateAsync(entity);
                await _unitOfWork.CommitAsync();
                return _mapper.Map<BlogPostDto>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating blog post with id {dto.Id}");
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Deleting blog post with id {id} from database");
                await _unitOfWork.BlogPostRepository.DeleteAsync(id);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting blog post with id {id} from database");
                throw;
            }
        }

    }
}
