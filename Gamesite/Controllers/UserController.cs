using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using Gamesite.Models;

namespace Gamesite.Controllers
{

    public class UserController : Controller
    {

        [HttpGet("/sign-in")]
        public ActionResult SignIn()
        {
          return View();
        }

        // find active user
        [HttpPost("/sign-in")]
        public ActionResult ValidateSignIn(string email, string password)
        {
          User newUser = User.Find(email, password);
          newUser.ActiveUser = true;
          return RedirectToAction("/", newUser);
        }

        // SIGN OUT - Only will set ActiveUser to false
        [HttpPost("/sign-out")]
        public ActionResult SignOut(User user)
        {
          user.ActiveUser = false;
          return RedirectToAction("/");
        }
        [HttpGet("/sign-up")]
        public ActionResult SignUp()
        {
          return View();
        }

        [HttpPost("/sign-up")]
        public ActionResult Create(string username, string email, string password)
        {
          User newUser = new User(username, email, password);
          if(newUser.CheckDuplicateUsername() == false)
          {
            // tell screen username has been taken
            return RedirectToAction("SignUp");
          }
          else {
            newUser.Save();
            return RedirectToAction("SignIn");
          }
        }


        // // [HttpGet("/countries/{id}")]
        // // public ActionResult Show(string id)
        // // {
        // //     List<Country> country = Country.GetAll();
        // //     return View(country);
        // // }

    }

}
