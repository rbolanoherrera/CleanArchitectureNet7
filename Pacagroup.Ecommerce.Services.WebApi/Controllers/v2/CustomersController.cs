using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pacagroup.Ecommerce.Application.DTO;
using Pacagroup.Ecommerce.Application.Interface.UseCases;
using System.Threading.Tasks;


namespace Pacagroup.Ecommerce.Services.WebApi.Controllers.v2
{
    /// <summary>
    /// Adminsitrar los clientes
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("2.0")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerApplication customerApplication;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="customerApplication"></param>
        public CustomersController(ICustomerApplication customerApplication)
        {
            this.customerApplication = customerApplication;
        }

        #region "Metodos Sincronos"

        /// <summary>
        /// ingresar clientes a la BD
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPost("insert")]
        public IActionResult Insert([FromBody] CustomerDTO customer)
        {
            if (customer == null)
                return BadRequest();

            var response = customerApplication.Insert(customer);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response.Message);
        }

        /// <summary>
        /// Actualizar información del usuario
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPut("update/{customerId}")]
        public IActionResult Update(string customerId,[FromBody] CustomerDTO customer)
        {
            if(string.IsNullOrEmpty(customerId))
                return BadRequest();

            var objCustomer = customerApplication.Get(customerId);

            if(objCustomer.Data == null)
                return NotFound(objCustomer.Message);

            if (customer == null)
                return BadRequest();

            var response = customerApplication.Update(customer);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response.Message);
        }

        [HttpDelete("delete")]
        public IActionResult Delete(string customerId)
        {
            if (string.IsNullOrEmpty(customerId))
                return BadRequest();

            var response = customerApplication.Delete(customerId);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response.Message);
        }

        [HttpGet("get/{customerId}")]
        public IActionResult Get(string customerId)
        {
            if (string.IsNullOrEmpty(customerId))
                return BadRequest();

            var response = customerApplication.Get(customerId);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response.Message);
        }

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var response = customerApplication.GetAll();
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response.Message);
        }

        #endregion

        #region "Metodos Asincronos"

        [HttpPost("insertAsync")]
        public async Task<IActionResult> InsertAsync(CustomerDTO customer)
        {
            if (customer == null)
                return BadRequest();

            var response = await customerApplication.InsertAsync(customer);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response.Message);
        }

        /// <summary>
        /// Actualizar información del usuario metodo asincrono
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPut("UpdateAsync/{customerId}")]
        public async Task<IActionResult> UpdateAsync(string customerId,[FromBody] CustomerDTO customer)
        {
            if (string.IsNullOrEmpty(customerId))
                return BadRequest();

            var objCustomer = await customerApplication.GetAsync(customerId);

            if (objCustomer.Data == null)
                return NotFound(objCustomer.Message);

            if (customer == null)
                return BadRequest();

            var response = await customerApplication.UpdateAsync(customer);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response.Message);
        }

        [HttpDelete("DeleteAsync")]
        public async Task<IActionResult> DeleteAsync(string customerId)
        {
            if (string.IsNullOrEmpty(customerId))
                return BadRequest();

            var response = await customerApplication.DeleteAsync(customerId);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response.Message);
        }

        [HttpGet("getAsync/{customerId}")]
        public async Task<IActionResult> GetAsync(string customerId)
        {
            if (string.IsNullOrEmpty(customerId))
                return BadRequest();

            var response = await customerApplication.GetAsync(customerId);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response.Message);
        }

        [HttpGet("getAllAsync")]
        public async Task<IActionResult> GetAllAsync()
        {
            var response = await customerApplication.GetAllAsync();
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response.Message);
        }


        #endregion "Fin Metodos Asincronos"

    }

}