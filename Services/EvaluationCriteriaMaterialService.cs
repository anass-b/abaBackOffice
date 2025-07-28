using AutoMapper;
using abaBackOffice.DTOs;
using abaBackOffice.Interfaces;
using abaBackOffice.Interfaces.Services;
using abaBackOffice.Models;
using Microsoft.Extensions.Logging;

namespace abaBackOffice.Services
{
    public class EvaluationCriteriaMaterialService : IEvaluationCriteriaMaterialService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<EvaluationCriteriaMaterialService> _logger;
        private readonly IMapper _mapper;

        public EvaluationCriteriaMaterialService(
            IUnitOfWork unitOfWork,
            ILogger<EvaluationCriteriaMaterialService> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EvaluationCriteriaMaterialDto>> GetAllAsync()
        {
            try
            {
                var list = await _unitOfWork.EvaluationCriteriaMaterialRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<EvaluationCriteriaMaterialDto>>(list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving EvaluationCriteriaMaterials");
                throw;
            }
        }

        public async Task<IEnumerable<EvaluationCriteriaMaterialDto>> GetByCriteriaIdAsync(int criteriaId)
        {
            try
            {
                var list = await _unitOfWork.EvaluationCriteriaMaterialRepository.GetByCriteriaIdAsync(criteriaId);
                return _mapper.Map<IEnumerable<EvaluationCriteriaMaterialDto>>(list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving EvaluationCriteriaMaterials for criteriaId {criteriaId}");
                throw;
            }
        }

        public async Task CreateAsync(EvaluationCriteriaMaterialDto dto)
        {
            try
            {
                var entity = _mapper.Map<EvaluationCriteriaMaterial>(dto);
                await _unitOfWork.EvaluationCriteriaMaterialRepository.CreateAsync(entity);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating EvaluationCriteriaMaterial");
                throw;
            }
        }

        public async Task DeleteByCriteriaIdAsync(int criteriaId)
        {
            try
            {
                await _unitOfWork.EvaluationCriteriaMaterialRepository.DeleteByCriteriaIdAsync(criteriaId);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting EvaluationCriteriaMaterials by criteriaId {criteriaId}");
                throw;
            }
        }
    }
}
