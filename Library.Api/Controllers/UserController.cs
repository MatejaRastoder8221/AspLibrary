using Library.Application.UseCases.Commands.Users;
using Library.Application.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Library.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRegisterUserCommand _registerUserCommand;

        public UserController(IRegisterUserCommand registerUserCommand)
        {
            _registerUserCommand = registerUserCommand;
        }

        // POST: api/user/register
        [HttpPost("register")]
        [AllowAnonymous] // Skip JWT authentication for registration
        public IActionResult Register([FromBody] RegisterUserDto dto)
        {
            try
            {
                _registerUserCommand.Execute(dto);
                return Ok("User registered successfully.");
            }
            catch (Exception ex)
            {
                // Log the exception (if logging is implemented)
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
