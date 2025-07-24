using AutoMapper;
using abaBackOffice.DTOs;
using abaBackOffice.Interfaces;
using abaBackOffice.Interfaces.Services;
using abaBackOffice.Models;
using Microsoft.Extensions.Logging;

namespace abaBackOffice.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CategoryService> _logger;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork unitOfWork, ILogger<CategoryService> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            try
            {
                var categories = await _unitOfWork.CategoryRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<CategoryDto>>(categories);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving categories");
                throw;
            }
        }

        public async Task<CategoryDto> GetByIdAsync(int id)
        {
            try
            {
                var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
                return _mapper.Map<CategoryDto>(category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving category with id {id}");
                throw;
            }
        }

        public async Task<CategoryDto> CreateAsync(CategoryDto dto)
        {
            try
            {
                var entity = _mapper.Map<Category>(dto);
                await _unitOfWork.CategoryRepository.CreateAsync(entity);
                await _unitOfWork.CommitAsync();
                return _mapper.Map<CategoryDto>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating category");
                throw;
            }
        }

        public async Task<CategoryDto> UpdateAsync(CategoryDto dto)
        {
            try
            {
                var entity = _mapper.Map<Category>(dto);
                await _unitOfWork.CategoryRepository.UpdateAsync(entity);
                await _unitOfWork.CommitAsync();
                return _mapper.Map<CategoryDto>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating category with id {dto.Id}");
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Deleting category with id {id}");
                await _unitOfWork.CategoryRepository.DeleteAsync(id);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting category with id {id}");
                throw;
            }
        }
    }
}
