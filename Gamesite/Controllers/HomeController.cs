using Microsoft.AspNetCore.Mvc;

namespace Gamesite.Controllers
{
    public class HomeController : Controller
    {

        [HttpGet("/")]
        public ActionResult Index()
        {
            return View();
        }

    }
}
