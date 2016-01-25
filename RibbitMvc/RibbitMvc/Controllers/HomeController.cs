using RibbitMvc.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RibbitMvc.Controllers
{
    public class HomeController : RibbitControllerBase
    {
        public HomeController() : base() { }

        public ActionResult Index()
        {
            if (!Security.IsAuthenticated)
            {
                return View("Landing", new LoginSignupViewModel());
            }

            var timeline = Ribbits.GetTimelineFor(Security.UserId).ToArray();

            return View("Timeline", timeline);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Follow(string username)
        {
            if (!Security.IsAuthenticated) 
            {
                return RedirectToAction("Index");
            }

            Users.Follow(username, Security.GetCurrentUser());

            return GoToReferrer();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Unfollow(string username)
        {
            if (!Security.IsAuthenticated)
            {
                return RedirectToAction("Index");
            }

            Users.Unfollow(username, Security.GetCurrentUser());

            return GoToReferrer();
        }

        public ActionResult Profiles()
        {
            var users = Users.All(true);

            return View(users);
        }

        public ActionResult Followers()
        {
            if (!Security.IsAuthenticated)
            {
                return RedirectToAction("Index");
            }

            var user = Users.GetAllFor(Security.UserId);

            return View("Buddies", new BuddiesViewModel()
            {
                User = user,
                Buddies = user.Followers
            });
        }

        public ActionResult Following()
        {
            if (!Security.IsAuthenticated)
            {
                return RedirectToAction("Index");
            }

            var user = Users.GetAllFor(Security.UserId);

            return View("Buddies", new BuddiesViewModel()
            {
                User = user,
                Buddies = user.Followings
            });
        }

        [HttpGet]
        [ChildActionOnly]
        public ActionResult Create()
        {
            return PartialView("_CreateRibbitPartial", new CreateRibbitViewModel());
        }

        [HttpPost]
        [ChildActionOnly]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateRibbitViewModel model)
        {
            if (ModelState.IsValid)
            {
                Ribbits.Create(Security.UserId, model.Status);

                Response.Redirect("/");
            }

            return PartialView("_CreateRibbitPartial", model);
        }

    }
}
