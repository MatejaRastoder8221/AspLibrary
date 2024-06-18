using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Library.DataAccess;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace Library.Api.Core
{
    public class JwtTokenCreator
    {
        private readonly AspContext _context;
        private readonly ITokenStorage _storage;
        private readonly string _issuer;
        private readonly string _secretKey;
        private readonly int _seconds;

        public JwtTokenCreator(AspContext context, ITokenStorage storage, string issuer, string secretKey, int seconds)
        {
            _context = context;
            _storage = storage;
            _issuer = issuer;
            _secretKey = secretKey;
            _seconds = seconds;
        }

        public string Create(string email, string password)
        {
            var user = _context.Users.Where(x => x.Email == email).Select(x => new
            {
                x.Username,
                x.Password,
                x.FirstName,
                x.LastName,
                x.Id,
                UseCaseIds = x.UserUseCases.Select(x => x.UseCaseId)
            }).FirstOrDefault();

            if (user == null)
            {
                throw new UnauthorizedAccessException();
            }

            if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                throw new UnauthorizedAccessException();
            }

            Guid tokenGuid = Guid.NewGuid();

            string tokenId = tokenGuid.ToString();

            var claims = new List<Claim>
            {
                 new Claim(JwtRegisteredClaimNames.Jti, tokenId, ClaimValueTypes.String),
                 new Claim(JwtRegisteredClaimNames.Iss, _issuer, ClaimValueTypes.String),
                 new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
                 new Claim("Username", user.Username),
                 new Claim("FirstName", user.FirstName),
                 new Claim("LastName", user.LastName),
                 new Claim("Id", user.Id.ToString()),
                 new Claim("UseCaseIds", JsonConvert.SerializeObject(user.UseCaseIds)),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var now = DateTime.UtcNow;
            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: "Any",
                claims: claims,
                notBefore: now,
                expires: now.AddSeconds(_seconds),
                signingCredentials: credentials);

            _storage.Add(tokenGuid);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
