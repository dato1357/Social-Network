using RibbitMvc.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RibbitMvc.Controllers
{
    public class UserController : RibbitControllerBase
    {
        //
        // GET: /User/

        public ActionResult Index(string username)
        {
            var user = Users.GetAllFor(username);

            if (user == null)
            {
                return new HttpNotFoundResult();
            }

            return View("UserProfile", new UserViewModel()
            {
                User = user,
                Ribbits = user.Ribbits
            });
        }

        public ActionResult Followers(string username)
        {
            var user = Users.GetAllFor(username);

            if (user == null)
            {
                return new HttpNotFoundResult();
            }

            return View("Buddies", new BuddiesViewModel()
            {
                User = user,
                Buddies = user.Followers
            });
        }

        public ActionResult Following(string username)
        {
            var user = Users.GetAllFor(username);

            if (user == null)
            {
                return new HttpNotFoundResult();
            }

            return View("Buddies", new BuddiesViewModel()
            {
                User = user,
                Buddies = user.Followings
            });
        }

    }
}
