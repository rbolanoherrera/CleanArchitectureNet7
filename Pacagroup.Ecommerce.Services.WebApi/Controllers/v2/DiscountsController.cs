using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pacagroup.Ecommerce.Application.DTO;
using Pacagroup.Ecommerce.Application.Interface.UseCases;
using System.Threading.Tasks;

namespace Pacagroup.Ecommerce.Services.WebApi.Controllers.v2
{
    /// <summary>
    /// Controlador de Descuentos
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("2.0")]
    public class DiscountsController : ControllerBase
    {
        private readonly IDiscountsApplication discountsApplication;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="discountsApplication"></param>
        public DiscountsController(IDiscountsApplication discountsApplication)
        {
            this.discountsApplication = discountsApplication;
        }

        /// <summary>
        /// insertar un nuevo descuento
        /// </summary>
        /// <param name="discount"></param>
        /// <returns></returns>
        [HttpPost("insert")]
        public async Task<IActionResult> Insert([FromBody] DiscountDTO discount)
        {
            if (discount == null)
                return BadRequest();

            var response = await discountsApplication.Create(discount);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response.Message);
        }

        /// <summary>
        /// Obtener todos los descuentos
        /// </summary>
        /// <returns></returns>
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var response = await discountsApplication.GetAll();
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response.Message);
        }

    }
}