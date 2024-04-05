using Microsoft.AspNetCore.Mvc;

namespace Pacagroup.Ecommerce.Services.WebApi.Controllers
{
    [Route("[controller]")]
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {

            return View();
        }
    }
}
