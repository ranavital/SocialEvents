using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using SocialEvents.Models;
using SocialEvents.ViewModel;

namespace SocialEvents.Repositories
{
    public class ContentRepository
    {
        private readonly DBContext db = new DBContext();
        public int UploadImageInDataBase(HttpPostedFileBase file, SituationViewModel situationVM)
        {
            situationVM.Image = ConvertToBytes(file);
            var situation = new Situation
            {
                CorrectAnswer = situationVM.CorrectAnswer,
                Description = situationVM.Description,
                IncorrectAnswer1 = situationVM.IncorrectAnswer1,
                IncorrectAnswer2 = situationVM.IncorrectAnswer2,
                CorrectDescription = situationVM.CorrectDescription,
                IncorrectDescription1 = situationVM.IncorrectDescription1,
                IncorrectDescription2 = situationVM.IncorrectDescription2,
                Image = situationVM.Image
            };
            db.Situations.Add(situation);
            int i = db.SaveChanges();
            if (i == 1)
            {
                return 1;
            }
            else
            {
                return 0;
            }

        }

        public byte[] ConvertToBytes(HttpPostedFileBase image)
        {
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(image.InputStream);
            imageBytes = reader.ReadBytes((int)image.ContentLength);
            return imageBytes;
        }
    }
}