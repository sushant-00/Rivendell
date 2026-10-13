using Application.IServices;
using Domain.Entites;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Services
{
    public class TokenServices(IConfiguration config) : ITokenServices
    {
        public string CreateToken(AppUsers user)
        {
            var TokenKey = config["TokenKey"] ?? throw new Exception("Cannot get tokenkey");
            if(TokenKey.Length <= 64)
            {
                throw new Exception("key length should be greater thaan 64");
                
                
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenKey));

            var claims = new List<Claim>()
                {
                    new (ClaimTypes.Email , user.Email),
                    new (ClaimTypes.NameIdentifier,user.Id)
                };

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var TokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = creds
            };

            var TokenHandler = new JwtSecurityTokenHandler();
            var Token = TokenHandler.CreateToken(TokenDescriptor);
            return TokenHandler.WriteToken(Token);
        }
    }
}
