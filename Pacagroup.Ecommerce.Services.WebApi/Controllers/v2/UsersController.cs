using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
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
using System.Threading.Tasks;

namespace Pacagroup.Ecommerce.Services.WebApi.Controllers.v2
{
    /// <summary>
    /// Contoller de Usuarios del sistema
    /// </summary>
    [Authorize]
    [ApiController]
    [EnableRateLimiting("fixedWindow")]
    [Route("api/[controller]")]
    [ApiVersion("2.0")]
    public class UsersController : Controller
    {
        private readonly IUserApplication userApplication;
        private readonly IAppLogger<UsersController> logger;
        private readonly AppSettings appSettings;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userApplication"></param>
        /// <param name="appSettings"></param>
        /// <param name="logger"></param>
        public UsersController(IUserApplication userApplication,
            IOptions<AppSettings> appSettings,
            IAppLogger<UsersController> logger)
        {
            this.userApplication = userApplication;
            this.logger = logger;
            this.appSettings = appSettings.Value;
        }

        /// <summary>
        /// Autenticarse en la aplicación. Validación del Usuario y contraseña
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Obtener todos los Usuarios
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var response = userApplication.GetAll();

            if (response.Data != null)
                return Ok(response);
            else
                return NotFound(response);
        }

        /// <summary>
        /// Obtener todos los Usuarios metodo asincrono
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllSync")]
        public async Task<IActionResult> GetAllSync()
        {
            var response = await userApplication.GetAllAsync();

            if (response.Data != null)
                return Ok(response);
            else
                return NotFound(response);
        }

        /// <summary>
        /// Crear un nuevo Usuario
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("Insert")]
        public IActionResult Insert([FromBody] UserDTO user)
        {
            var response = userApplication.Insert(user);

            if (response.IsSuccess)
                return Ok(response);
            else
                return BadRequest(response);
        }

        /// <summary>
        /// Crear un nuevo Usuario, metodo asincrono
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("InsertAsync")]
        public async Task<IActionResult> InsertAsync([FromBody] UserDTO user)
        {
            var response = await userApplication.InsertASync(user);

            if (response.IsSuccess)
                return Ok(response);
            else
                return BadRequest(response);
        }

    }
}
