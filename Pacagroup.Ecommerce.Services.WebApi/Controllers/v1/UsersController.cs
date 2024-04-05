using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Pacagroup.Ecommerce.Application.DTO;
using Pacagroup.Ecommerce.Application.Interface;
using Pacagroup.Ecommerce.Services.WebApi.Helpers;
using Pacagroup.Ecommerce.Transversal.Common;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Pacagroup.Ecommerce.Services.WebApi.Controllers.v1
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class UsersController : Controller
    {
        private readonly IUserApplication userApplication;
        private readonly IAppLogger<UsersController> logger;
        private readonly AppSettings appSettings;

        public UsersController(IUserApplication userApplication,
            IOptions<AppSettings> appSettings,
            IAppLogger<UsersController> logger)
        {
            this.userApplication = userApplication;
            this.logger = logger;
            this.appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("[action]")]
        public IActionResult Authenticate([FromBody] UserDTO userDTO)
        {
            logger.LogInformation($"entro a autenticarse {DateTime.Now}");

            var response = userApplication.Authenticate(userDTO.UserName, userDTO.Password);

            if (response.Data != null)
            {
                response.Data.Token = BuildToken(response);

                return Ok(response);
            }
            else
                return NotFound(response);
        }

        private string BuildToken(Response<UserDTO> userDTO)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userDTO.Data.UserId.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature),
                Issuer = appSettings.Issuer,
                Audience = appSettings.Audience
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            string tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }

    }
}
