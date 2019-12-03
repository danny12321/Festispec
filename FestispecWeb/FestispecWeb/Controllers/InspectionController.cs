using Festipec.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace FestispecWeb.Controllers
{
    public class InspectionController : Controller
    {
        FestispecEntities db = new FestispecEntities();

        public ActionResult Inspections()
        {
            return View(db.Inspections.ToList());
        }

        public ActionResult Questionnaires(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var inspection = db.Inspections.Find(id);

            if(inspection == null)
            {
                return HttpNotFound();
            }

            return View(db.Questionnaires.ToList().Where(s => s.inspection_id == id));
        }

        public ActionResult questionnaire(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var questionaires = db.Questionnaires.Find(id);

            if(questionaires == null)
            {
                return HttpNotFound();
            }
            return View(db.Questions.ToList().Where(s => s.questionnaire_id == id));
        }
    }
}