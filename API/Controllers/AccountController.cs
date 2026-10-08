using Domain.Entites;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(AppDbContext context) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<ActionResult<AppUsers>> Register(string email, string displayName, string password) {
            var hmac = new HMACSHA512();
            var user = new AppUsers
            {
                DisplayName = displayName,
                Email = email,
                PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)),
                PasswordSalt = hmac.Key


            };

            context.Users.Add(user);
            await context.SaveChangesAsync();
            return user;
                }


    }
}
