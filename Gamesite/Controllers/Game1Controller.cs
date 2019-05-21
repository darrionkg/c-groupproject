using Microsoft.AspNetCore.Mvc;
using Gamesite.Models;
using System.Collections.Generic;

namespace GameSite.Controllers
{

    public class Game1Controller : Controller
    {

        [HttpGet("/Game1")]
        public ActionResult Game1()
        {
            return View();
        }

        [HttpGet("/Game1/Play")]
        public ActionResult Play()
        {
          return View();
        }

    }

}
