// -------------------- ReinforcementProgramService --------------------
using AutoMapper;
using abaBackOffice.DTOs;
using abaBackOffice.Interfaces;
using abaBackOffice.Interfaces.Services;
using abaBackOffice.Models;
using Microsoft.Extensions.Logging;

namespace abaBackOffice.Services
{
    public class ReinforcementProgramService : IReinforcementProgramService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ReinforcementProgramService> _logger;
        private readonly IMapper _mapper;

        public ReinforcementProgramService(IUnitOfWork unitOfWork, ILogger<ReinforcementProgramService> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReinforcementProgramDto>> GetAllAsync()
        {
            try
            {
                var programs = await _unitOfWork.ReinforcementProgramRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<ReinforcementProgramDto>>(programs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving reinforcement programs");
                throw;
            }
        }

        public async Task<ReinforcementProgramDto> GetByIdAsync(int id)
        {
            try
            {
                var program = await _unitOfWork.ReinforcementProgramRepository.GetByIdAsync(id);
                return _mapper.Map<ReinforcementProgramDto>(program);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving reinforcement program with id {id}");
                throw;
            }
        }

        public async Task<ReinforcementProgramDto> CreateAsync(ReinforcementProgramDto dto)
        {
            try
            {
                var entity = _mapper.Map<ReinforcementProgram>(dto);
                await _unitOfWork.ReinforcementProgramRepository.CreateAsync(entity);
                await _unitOfWork.CommitAsync();
                return _mapper.Map<ReinforcementProgramDto>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating reinforcement program");
                throw;
            }
        }

        public async Task<ReinforcementProgramDto> UpdateAsync(ReinforcementProgramDto dto)
        {
            try
            {
                var entity = _mapper.Map<ReinforcementProgram>(dto);
                await _unitOfWork.ReinforcementProgramRepository.UpdateAsync(entity);
                await _unitOfWork.CommitAsync();
                return _mapper.Map<ReinforcementProgramDto>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating reinforcement program with id {dto.Id}");
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Deleting reinforcement program with id {id} from database");
                await _unitOfWork.ReinforcementProgramRepository.DeleteAsync(id);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting reinforcement program with id {id} from database");
                throw;
            }
        }

    }
}
