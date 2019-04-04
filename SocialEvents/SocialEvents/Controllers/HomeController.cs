using SocialEvents.Classes;
using SocialEvents.Dal;
using SocialEvents.Models;
using SocialEvents.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
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
                    return RedirectToAction("TherapistPage", "Situation");
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
                Encryption enc = new Encryption();
                UserDal usrDal = new UserDal();
                User objUser = usrDal.Users.FirstOrDefault<User>(x => x.Email == usr.Email);
                if (objUser == null)
                {
                    TherapistDal trpDal = new TherapistDal();
                    Therapist objTherapist = trpDal.Users.FirstOrDefault<Therapist>(x => x.Email == usr.Email);
                    if (objTherapist == null || !enc.ValidatePassword(usr.Password, objUser.Password))
                    {
                        ViewBag.errorUserLogin = "UserName or Password incorrect";
                        return View("LoginPage", usr);
                    }
                    Session["CurrentUser"] = objTherapist;
                    return RedirectToAction("RedirectByUser");
                }
                if (!enc.ValidatePassword(usr.Password, objUser.Password))
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
                    ViewBag.errorUserRegister = "Username already exists";
                    return View("SignUpPage");
                }
                Encryption enc = new Encryption();
                usr.Password = enc.CreateHash(usr.Password);
                usrDal.Users.Add(new User { Email = usr.Email, Password = usr.Password });
                usrDal.SaveChanges();
                ViewBag.registerSuccessMsg = "Signup succeeded!";
                return View("HomePage");
                //DoubleAuthentincationVM user = new DoubleAuthentincationVM(new User { Email = usr.Email, Password = usr.Password },GetNumber(usr.Email));
                //return View("DoubleAuthentincationPage", user);
              
            }
            else
            {
                usr.Password = "";
                return View("SignUpPage");
            }
        }

        private int GetNumber(string mailTo)
        {
            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.Timeout = 10000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("medicalcalendar123", "mc12345!");

            try //Mail built-in  validation function.
            {
                MailAddress m = new MailAddress(mailTo);
            }

            catch (FormatException)
            {
                Console.WriteLine("Are you sure that you entered a valid mail address? Try again please.");
                return 0;
            }
            Random rnd = new Random();
            int randNum = rnd.Next(10000, 100000);
            MailMessage mm = new MailMessage("medicalcalendar123@donotreply.com", mailTo, "Authentication Code for Medical-Calendar", "Authentication number is: " + randNum.ToString() + " .");
            mm.BodyEncoding = UTF8Encoding.UTF8;
            mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            client.Send(mm);
            Session["Trials"] = 3;
            return randNum;
                }
        [HttpPost]
        public ActionResult Accept(DoubleAuthentincationVM usr)
        {
            usr.Usr = (User)TempData["usr"];
            usr.number = (int)TempData["number"];
            if ((int)Session["Trials"] > 0)
            {
                TryValidateModel(usr);
                if (ModelState.IsValid)
                {
                    if (usr.number == usr.number2)
                    {
                        UserDal usrDal = new UserDal();
                        usrDal.Users.Add(new User { Email = usr.Usr.Email, Password = usr.Usr.Password });
                        usrDal.SaveChanges();
                        ViewBag.registerSuccessMsg = "Signup succeeded!";
                        return View("HomePage");
                    }
                    Session["Trials"]= ((int)Session["Trials"]) -1;
                    return View("DoubleAuthentincationPage", usr);
                }
                return View("DoubleAuthentincationPage", usr);
            }
            DoubleAuthentincationVM user = new DoubleAuthentincationVM(new User { Email = usr.Usr.Email, Password = usr.Usr.Password }, GetNumber(usr.Usr.Email));
            return View("DoubleAuthentincationPage", user);
        }
    }


    

}