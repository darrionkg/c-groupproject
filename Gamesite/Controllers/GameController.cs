using Microsoft.AspNetCore.Mvc;
using Gamesite.Models;
using System.Collections.Generic;

namespace GameSite.Controllers
{

    public class GameController : Controller
    {

        [HttpGet("/Game1")]
        public ActionResult Game1()
        {
            return View();
        }

    }

}
