using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pacagroup.Ecommerce.Application.Interface.UseCases;
using System.Threading.Tasks;

namespace Pacagroup.Ecommerce.Services.WebApi.Controllers.v2
{
    /// <summary>
    /// Administrar las Categorias usando Redis Cache
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("2.0")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesApplication categoriesApplication;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="categoriesApplication"></param>
        public CategoriesController(ICategoriesApplication categoriesApplication)
        {
            this.categoriesApplication = categoriesApplication;
        }

        /// <summary>
        /// retornar todas las categorias
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var categories = categoriesApplication.GetAll();

            if(categories.IsSuccess)
                return Ok(categories);
            else
                return BadRequest(categories);
        }

        [HttpGet("GetforList")]
        public async Task<IActionResult> GetForList()
        {
            var categories = await categoriesApplication.GetForList();

            if (categories.IsSuccess)
                return Ok(categories);
            else
                return BadRequest(categories);
        }
    }
}