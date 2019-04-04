using SocialEvents.Dal;
using SocialEvents.Models;
using SocialEvents.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocialEvents.Controllers
{

    public class HomeController : Controller
    {

        public ActionResult RedirectByUser()
        {
            if (Session["CurrentUser"] != null)
            {
                Object currentUsr = (Object)(Session["CurrentUser"]);
                if (currentUsr is User)
                    return RedirectToAction("UserPage", "User");
                else
                    return RedirectToAction("TherapistPage", "Therapst");
            }
            else
            {
                TempData["notAuthorized"] = "אין הרשאה!";
                return RedirectToAction("HomePage");
            }
        }


        public ActionResult HomePage()
        {
            if (Session["CurrentUser"] != null)
                return RedirectToAction("RedirectByUser");
            return View();
        }
        public ActionResult LoginPage()
        {
            if (Session["CurrentUser"] != null)
                return RedirectToAction("RedirectByUser");
            return View(new UserLogin());
        }
        [HttpPost]
        public ActionResult Login(UserLogin usr)
        {
            if (Session["CurrentUser"] != null)
                return RedirectToAction("RedirectByUser");
            if (ModelState.IsValid)
            {
                UserDal usrDal = new UserDal();
                User objUser = usrDal.Users.FirstOrDefault<User>(x => x.Email == usr.Email);
                if (objUser == null)
                {
                    TherapistDal trpDal = new TherapistDal();
                    Therapist objTherapist = trpDal.Users.FirstOrDefault<Therapist>(x => x.Email == usr.Email);
                    if (objTherapist == null || objTherapist.Password != usr.Password)
                    {
                        ViewBag.errorUserLogin = "UserName or Password incorrect";
                        return View("LoginPage", usr);
                    }
                    Session["CurrentUser"] = objTherapist;
                    return RedirectToAction("RedirectByUser");
                }
                if (objUser.Password != usr.Password)
                {
                    ViewBag.errorUserLogin = "UserName or Password incorrect";
                    return View("LoginPage", usr);
                }
                objUser.Password = "";
                Session["CurrentUser"] = objUser;
                return RedirectToAction("RedirectByUser");
            }
            else
            {
                usr.Password = "";
                return View("LoginPage", usr);
            }
        }

        public ActionResult Logout()
        {
            Session["CurrentUser"] = null;
            return RedirectToAction("RedirectByUser");
        }
        public ActionResult SignUpPage()
        {
            if (Session["CurrentUser"] != null)
                return RedirectToAction("RedirectByUser");
            return View(new VMUserRegister());
        }
        [HttpPost]
        public ActionResult SignUp(VMUserRegister usr)
        {
            if (Session["CurrentUser"] != null)
                return RedirectToAction("RedirectByUser");
            usr.Password = usr.Password;
            ModelState.Clear();
            TryValidateModel(usr);
            if (ModelState.IsValid)
            {
                UserDal usrDal = new UserDal();
                TherapistDal trpDal = new TherapistDal();
                User objUser = usrDal.Users.FirstOrDefault<User>(x => x.Email == usr.Email);
                Therapist objTherapist = trpDal.Users.FirstOrDefault<Therapist>(x => x.Email == usr.Email);
                if (objUser != null || objTherapist != null)
                {
                    ViewBag.errorUserRegister = "שם המשתמש שבחרת קיים";
                    return View("SignUpPage");
                }
                    usrDal.Users.Add(new User {Email=usr.Email, Password=usr.Password });
                    usrDal.SaveChanges();

                    ViewBag.registerSuccessMsg = "ההרשמה בוצעה בהצלחה!";
                    return View("HomePage");
                }
       
          
            else
            {
                usr.Password = "";
                return View("SignUpPage");
            }
        }
    }

}