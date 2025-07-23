using abaBackOffice.Interfaces.Services;
using abaBackOffice.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace abaBackOffice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OtpCodeController : ControllerBase
    {
        private readonly IOtpCodeService _otpCodeService;
        private readonly ILogger<OtpCodeController> _logger;

        public OtpCodeController(IOtpCodeService otpCodeService, ILogger<OtpCodeController> logger)
        {
            _otpCodeService = otpCodeService;
            _logger = logger;
        }

        [HttpGet("by-code/{code}")]
        public async Task<ActionResult<OtpCodeDto>> GetOtpCodeByCode(string code)
        {
            _logger.LogInformation($"Retrieving OTP code by code: {code}");
            var otp = await _otpCodeService.GetByCodeAsync(code);
            if (otp == null)
            {
                _logger.LogWarning($"OTP with code {code} not found");
                return NotFound();
            }
            return Ok(otp);
        }

        [HttpPost]
        public async Task<ActionResult<OtpCodeDto>> CreateOtpCode(OtpCodeDto otpCodeDto)
        {
            _logger.LogInformation("Creating a new OTP code");
            var createdOtp = await _otpCodeService.CreateAsync(otpCodeDto);
            return Created("", createdOtp); // No need for GetById route since we only use `code`
        }

        [HttpPost("validate")]
        public async Task<ActionResult<bool>> ValidateOtpCode([FromQuery] string code, [FromQuery] string email)
        {
            _logger.LogInformation($"Validating OTP code {code} for email {email}");
            var isValid = await _otpCodeService.ValidateCodeAsync(code, email);
            return Ok(isValid);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOtpCode(int id)
        {
            _logger.LogInformation($"Deleting OTP code with id {id}");
            await _otpCodeService.DeleteAsync(id);
            return NoContent();
        }
    }
}
