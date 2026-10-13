using Application.Dtos;
using Application.Extensions;
using Application.IServices;
using Domain.Entites;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(AppDbContext context , ITokenServices tokenServices) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDto dto) {
            if(await EmailExits(dto.email)) return BadRequest("email taken");
            var hmac = new HMACSHA512();
            var user = new AppUsers
            {
                DisplayName = dto.displayName,
                Email = dto.email,
                PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(dto.password)),
                PasswordSalt = hmac.Key


            };

            context.Users.Add(user);
            await context.SaveChangesAsync();
            return user.ToDto(tokenServices);
                }

        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDto loginDto)
        {
            var user = await context.Users.SingleOrDefaultAsync(x => x.Email.ToLower() == loginDto.email);
            if (user != null)
            {
                using var hmac = new HMACSHA512(user.PasswordSalt);
                var hashedcomaper = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.password));
                for(int i = 0; i < hashedcomaper.Length; i++)
                {
                    if (hashedcomaper[i] != user.PasswordHash[i])
                    {
                        return Unauthorized("invalid password");
                    }

                }




            }
            else
            {
                return Unauthorized("user not found !");
            }

            return user.ToDto(tokenServices);


        }

        public async Task<bool> EmailExits(string email)
        {
            return await context.Users.AnyAsync(x => x.Email.ToLower() == email.ToLower());
            
        }



    }
}
