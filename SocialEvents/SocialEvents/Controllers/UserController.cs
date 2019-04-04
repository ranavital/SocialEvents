using SocialEvents.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocialEvents.Controllers {
    public class UserController : Controller {
        private bool Authorize() {
            object currentUser = (object)(Session["CurrentUser"]);
            if (currentUser is User)
                return true;
            else
                return false;
        }
        public ActionResult UserPage() {
            if (!Authorize())
                return RedirectToAction("RedirectByUser","Home");
            return View();
        }
    }
}