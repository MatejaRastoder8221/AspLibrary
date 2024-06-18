using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Library.Application;
using Library.Implementation;
using Newtonsoft.Json; // Import your Actor class here

namespace Library.Api.Core
{
    public class JwtApplicationActorProvider : IApplicationActorProvider
    {
        private string authorizationHeader;

        public JwtApplicationActorProvider(string authorizationHeader)
        {
            this.authorizationHeader = authorizationHeader;
        }

        public IApplicationActor GetActor()
        {
            if (authorizationHeader.Split("Bearer ").Length != 2)
            {
                return new UnauthorizedActor();
            }

            string token = authorizationHeader.Split("Bearer ")[1];

            var handler = new JwtSecurityTokenHandler();

            var tokenObj = handler.ReadJwtToken(token);

            var claims = tokenObj.Claims;

            var actor = new Actor
            {
                Email = claims.First(x => x.Type == "Username").Value,
                Username = claims.First(x => x.Type == "Username").Value,
                FirstName = claims.First(x => x.Type == "FirstName").Value,
                LastName = claims.First(x => x.Type == "LastName").Value,
                Id = int.Parse(claims.First(x => x.Type == "Id").Value),
                AllowedUseCases = JsonConvert.DeserializeObject<List<int>>(claims.First(x => x.Type == "UseCaseIds").Value)
            };

            return actor;
        }
    }
}
