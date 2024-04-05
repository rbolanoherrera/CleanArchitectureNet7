using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pacagroup.Ecommerce.Application.DTO;
using Pacagroup.Ecommerce.Application.Interface;
using System.Threading.Tasks;


namespace Pacagroup.Ecommerce.Services.WebApi.Controllers.v1
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerApplication customerApplication;

        public CustomersController(ICustomerApplication customerApplication)
        {
            this.customerApplication = customerApplication;
        }

        #region "Metodos Sincronos"

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

        [HttpPut("update")]
        public IActionResult Update(CustomerDTO customer)
        {
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

        [HttpPut("UpdateAsync")]
        public async Task<IActionResult> UpdateAsync(CustomerDTO customer)
        {
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