using Library.Api.DTO;
using Library.Api.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Library.API.Core;

namespace Library.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtTokenCreator _tokenCreator;

        public AuthController(JwtTokenCreator tokenCreator)
        {
            _tokenCreator = tokenCreator;
        }

        // POST api/auth
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Post([FromBody] AuthRequest request)
        {
            string token = _tokenCreator.Create(request.Email, request.Password);

            if (token == null)
            {
                return Unauthorized(new { message = "Invalid email or password." });
            }

            return Ok(new AuthResponse { Token = token });
        }

        [Authorize]
        [HttpDelete]
        public IActionResult Delete([FromServices] ITokenStorage storage)
        {
            storage.Remove(this.Request.GetTokenId().Value);

            return NoContent();
        }
    }
}
