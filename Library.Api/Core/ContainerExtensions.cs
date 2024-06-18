using Library.Application;
using Library.Application.UseCases.Commands.Users;
using Library.Implementation;
using Library.Implementation.Validators;
using System.IdentityModel.Tokens.Jwt;
using Library.Implementation.UseCases.Commands;

namespace Library.API.Core
{
    public static class ContainerExtensions
    {
        public static void AddUseCases(this IServiceCollection services)
        {
            services.AddTransient<IRegisterUserCommand, EfRegisterUserCommand>();
            services.AddTransient<RegisterUserDtoValidator>();
        }

        public static Guid? GetTokenId(this HttpRequest request)
        {
            if (request == null || !request.Headers.ContainsKey("Authorization"))
            {
                return null;
            }

            string authHeader = request.Headers["Authorization"].ToString();

            if (authHeader.Split("Bearer ").Length != 2)
            {
                return null;
            }

            string token = authHeader.Split("Bearer ")[1];

            var handler = new JwtSecurityTokenHandler();

            var tokenObj = handler.ReadJwtToken(token);

            var claims = tokenObj.Claims;

            var claim = claims.First(x => x.Type == "jti").Value;

            var tokenGuid = Guid.Parse(claim);

            return tokenGuid;
        }
    }
}
