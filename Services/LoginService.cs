using helpmeinvest.Enums;
using helpmeinvest.Models;
using helpmeinvest.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace helpmeinvest.Services
{
    public class LoginService
    {
        private IConfiguration _config;
        private CredentialRepo _repo;

        public LoginService(IConfiguration config, CredentialRepo repo)
        {
            _config = config;
            _repo = repo;
        }

        public string GenerateJSONWebToken(Credential cred)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.UniqueName, cred.UserName),
                new Claim(JwtRegisteredClaimNames.Email, cred.Email),
                new Claim(JwtRegisteredClaimNames.Typ, cred.Role.ToString())
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public Credential AuthenticateUser(Credential cred)
        {
            return _repo.IsValidCredential(cred);
        }
        
        public string CreateCredentials(Credential cred, ClaimsPrincipal claims)
        {
            if (claims.HasClaim(c => c.Type == JwtRegisteredClaimNames.Typ) && cred.CustomerId != null)
            {
                // Make sure the person creating the credentials is admin
                var role = claims.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Typ).Value;
                if (role.Equals(Role.Admin.ToString()))
                {
                    var credential = _repo.Create(cred);
                    if (credential != null)
                    {
                        return GenerateJSONWebToken(credential);
                    }
                }
            }
            return string.Empty;
        }
    }
}
