using SocialEvents.Models;
using SocialEvents.Repositories;
using SocialEvents.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocialEvents.Controllers
{
    public class SituationController : Controller
    {
        private DBContext db = new DBContext();
        private bool Authorize()
        {
            object currentUser = (object)(Session["CurrentUser"]);
            if (currentUser is Therapist)
                return true;
            else
                return false;
        }

        public ActionResult TherapistPage()
        {
            if (!Authorize())
                return RedirectToAction("RedirectByUser", "Home");
            return View();
        }

        [Route("AddSituation")]
        [HttpGet]
        public ActionResult AddSituation()
        {
            var situation = db.Situations.Select(s => new
            {
                s.SolutionID,
                s.Description,
                s.CorrectAnswer,
                s.IncorrectAnswer1,
                s.IncorrectAnswer2,
                s.CorrectDescription,
                s.IncorrectDescription1,
                s.IncorrectDescription2,
                s.Image
            });
            List<SituationViewModel> situationVM = situation.Select(item => new SituationViewModel()
            {
                SolutionID = item.SolutionID,
                Description = item.Description,
                CorrectAnswer = item.CorrectAnswer,
                IncorrectAnswer1 = item.IncorrectAnswer1,
                IncorrectAnswer2 = item.IncorrectAnswer2,
                CorrectDescription = item.CorrectDescription,
                IncorrectDescription1 = item.IncorrectDescription1,
                IncorrectDescription2 = item.IncorrectDescription2,
                Image = item.Image
            }).ToList();
            return View(situationVM);
        }
        public ActionResult RetrieveImage(int id)
        {
            byte[] cover = GetImageFromDataBase(id);
            if (cover != null)
            {
                return File(cover, "image/jpg");
            }
            return null;
        }

        private byte[] GetImageFromDataBase(int id)
        {
            var q = from temp in db.Situations where temp.SolutionID == id select temp.Image;
            byte[] cover = q.First();
            return cover;
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [Route("Create")]
        [HttpPost]
        public ActionResult Create(SituationViewModel situationVM)
        {
            HttpPostedFileBase file = Request.Files["ImageData"];
            ContentRepository service = new ContentRepository();
            int i = service.UploadImageInDataBase(file, situationVM);
            if (i == 1)
            {
                return RedirectToAction("AddSituation");
            }
            return View(situationVM);
        }
    }
}