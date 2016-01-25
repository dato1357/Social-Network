using RibbitMvc.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RibbitMvc.Controllers
{
    public class AccountController : RibbitControllerBase
    {
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(LoginSignupViewModel model)
        {
            if (Security.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            model.Login = new LoginViewModel();

            var signup = model.Signup;

            if (!ModelState.IsValid)
            {
                return View("Landing", model);
            }

            if (Security.DoesUserExist(signup.Username))
            {
                ModelState.AddModelError("Username", "Username is already taken.");

                return View("Landing", model);
            }

            Security.CreateUser(signup);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginSignupViewModel model)
        {
            if (Security.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            model.Signup = new SignupViewModel();

            var login = model.Login;

            if (!ModelState.IsValid)
            {
                return View("Landing", model);
            }

            if (!Security.Authenticate(login.Username, login.Password))
            {
                ModelState.AddModelError("Username", "Username and/or password was incorrect.");

                return View("Landing", model);
            }

            Security.Login(login.Username);

            return GoToReferrer();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            Security.Logout();

            return RedirectToAction("Index", "Home");
        }
    }
}