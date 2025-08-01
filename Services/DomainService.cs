using abaBackOffice.DTOs;
using abaBackOffice.Interfaces;
using abaBackOffice.Interfaces.Services;
using abaBackOffice.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace abaBackOffice.Services
{
    public class DomainService : IDomainService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<DomainService> _logger;

        public DomainService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<DomainService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<DomainDto>> GetAllAsync()
        {
            var entities = await _unitOfWork.DomainRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<DomainDto>>(entities);
        }

        public async Task<DomainDto> GetByIdAsync(int id)
        {
            var entity = await _unitOfWork.DomainRepository.GetByIdAsync(id);
            return _mapper.Map<DomainDto>(entity);
        }

        public async Task<IEnumerable<DomainDto>> GetByCategoryIdAsync(int categoryId)
        {
            var query = _unitOfWork.DomainRepository.GetQueryable();
            var filtered = await query.Where(d => d.CategoryId == categoryId).ToListAsync();
            return _mapper.Map<IEnumerable<DomainDto>>(filtered);
        }

        public async Task<DomainDto> CreateAsync(DomainDto dto)
        {
            var entity = _mapper.Map<Domain>(dto);
            await _unitOfWork.DomainRepository.CreateAsync(entity);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<DomainDto>(entity);
        }

        public async Task<DomainDto> UpdateAsync(DomainDto dto)
        {
            var entity = _mapper.Map<Domain>(dto);
            await _unitOfWork.DomainRepository.UpdateAsync(entity);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<DomainDto>(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _unitOfWork.DomainRepository.DeleteAsync(id);
            await _unitOfWork.CommitAsync();
        }
    }
}
