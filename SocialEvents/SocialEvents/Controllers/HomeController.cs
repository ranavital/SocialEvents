using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

<<<<<<< HEAD
namespace SocialEvents.Controllers {

    public class HomeController : Controller {

        public ActionResult RedirectByUser() {
            if (Session["CurrentUser"] != null) {
                Object currentUsr = (Object)(Session["CurrentUser"]);
                if(currentUsr is User)
                    return RedirectToAction("UserPage","User");
                else
                    return RedirectToAction("TherapistPage", "Therapst");
            }
            else  
                return RedirectToAction("HomePage");
=======
namespace SocialEvents.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
>>>>>>> 69e62b384e0d1c24adab5b0dd7151dad9dc09546
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

<<<<<<< HEAD
        public ActionResult Logout() {
            Session["CurrentUser"] = null;
            return RedirectToAction("RedirectByUser");
        }
        public ActionResult SignUpPage() {
            if (Session["CurrentUser"] != null)
                return RedirectToAction("RedirectByUser");
            return View(new VMUserRegister());
        }
        [HttpPost]
        public ActionResult SignUp(VMUserRegister usr) {

            if (Session["CurrentUser"] != null)
                return RedirectToAction("RedirectByUser");
            ModelState.Clear();
            TryValidateModel(usr);
            if (ModelState.IsValid) {
                UserDal usrDal = new UserDal();
                TherapistDal trpDal = new TherapistDal();
                User objUser = usrDal.Users.FirstOrDefault<User>(x=> x.Email==usr.Email );
                Therapist objTherapist = trpDal.Users.FirstOrDefault<Therapist>(x => x.Email == usr.Email);
                if (objUser != null || objTherapist!=null)
                {
                    ViewBag.errorUserRegister = "The user name is already exist";
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
=======
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
>>>>>>> 69e62b384e0d1c24adab5b0dd7151dad9dc09546
        }
    }
}