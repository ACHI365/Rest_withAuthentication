using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Registration.Data;
using Registration.Modules;
using Microsoft.IdentityModel.Tokens;

namespace Registration.Controllers
{
    [Route("api/Auth")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        public static string UserName = null!; 
        
        private readonly DataContext dataContext;

        public RegistrationController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        
        [HttpPost, Route("signIn")]
        public async Task<IActionResult> Login(User user)
        {
            if (user == null)
            {
                return BadRequest("Invalid client request");
            }
            var dbuser = await dataContext.Users.FindAsync(user.Username);
            
            if (dbuser != null && user.Username == dbuser.Username && user.Password == dbuser.Password)
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@2410"));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var tokenOptions = new JwtSecurityToken(
                    issuer: "DrDarkHouse",
                    audience: "https://localhost:7178",
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: signinCredentials
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                UserName = user.Username;
                return Ok(new { Token = tokenString });
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpPost, Route("signUp")]
        public async Task<IActionResult> AddHero(User user)
        {
            if (await dataContext.Users.FindAsync(user.Username) != null)
            {
                return BadRequest("User already exists");
            }
            dataContext.Users.Add(user);
            await dataContext.SaveChangesAsync();

            return Ok();
        }
    }
}