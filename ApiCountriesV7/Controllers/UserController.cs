using ApiCountriesV7.Data;
using ApiCountriesV7.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ApiCountriesV7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private ApiDbContext _dbContext;
        private readonly JWTSettings _jwtsettings;

        public UserController(ApiDbContext dbContext, IOptions<JWTSettings> jwtsettings)
        {
            // With this field now, we can access everything that's present in the API context class.
            _dbContext = dbContext;
            _jwtsettings = jwtsettings.Value;
        }

        [HttpPost("[action]")]
        public IActionResult LogIn([FromBody] User user)
        {

            var data = _dbContext.Users.Where(a => a.Email == user.Email && a.Password == MD5Hash(user.Password));

            if (data.Count() > 0)
            {
                UserWithToken userWithToken = null;
                
                RefreshToken refreshToken = GenerateRefreshToken();

                user.RefreshTokens.Add(refreshToken);
                _dbContext.SaveChanges();

                userWithToken = new UserWithToken(user);
                userWithToken.RefreshToken = refreshToken.Token;

                //sign your token here here..
                userWithToken.AccessToken = GenerateAccessToken(user.Id);
                userWithToken.Email = user.Email;

                return Ok(userWithToken);
            }
            else 
            {
                return BadRequest("Email or password invalid.");
            }
        }

        private string GenerateAccessToken(int userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtsettings.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, Convert.ToString(userId))
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private RefreshToken GenerateRefreshToken()
        {
            RefreshToken refreshToken = new RefreshToken();

            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                refreshToken.Token = Convert.ToBase64String(randomNumber);
            }
            refreshToken.ExpiryDate = DateTime.UtcNow.AddMonths(6);

            return refreshToken;
        }

        public static string MD5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text  
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

            //get hash result after compute it  
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits  
                //for each byte  
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }
    }
}
