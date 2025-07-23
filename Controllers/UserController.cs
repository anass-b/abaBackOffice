using abaBackOffice.DTOs;
using abaBackOffice.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace abaBackOffice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
        {
            _logger.LogInformation("Retrieving all users");
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUserById(int id)
        {
            _logger.LogInformation($"Retrieving user with id {id}");
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
            {
                _logger.LogWarning($"User with id {id} not found");
                return NotFound();
            }
            return Ok(user);
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDto userDto)
        {
            userDto.IsAdmin = false;
            userDto.IsVerified = false;

            var createdUser = await _userService.CreateAsync(userDto);
            return Ok(createdUser);
        }


        [HttpPost]
        public async Task<ActionResult<UserDto>> CreateUser(UserDto userDto)
        {
            _logger.LogInformation("Creating a new user");
            var createdUser = await _userService.CreateAsync(userDto);
            return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserDto userDto)
        {
            if (id != userDto.Id)
            {
                _logger.LogWarning("User ID mismatch");
                return BadRequest();
            }

            _logger.LogInformation($"Updating user with id {id}");
            var updatedUser = await _userService.UpdateAsync(userDto);
            return Ok(updatedUser);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            _logger.LogInformation($"Deleting user with id {id}");
            await _userService.DeleteAsync(id);
            return NoContent();
        }
    }
}
