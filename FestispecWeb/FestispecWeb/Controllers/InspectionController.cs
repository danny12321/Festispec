using Festipec.Domain;
using FestispecWeb.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            InspectionVM InspectionVM = new InspectionVM();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var inspection = db.Inspections.Find(id);

            if(inspection == null)
            {
                return HttpNotFound();
            }

            InspectionVM.Questionnaires = db.Questionnaires.ToList().Where(s => s.inspection_id == id);
            InspectionVM.Inspections = inspection;


            return View(InspectionVM);
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

            var qa = questionaires.Questions.Select(q => new AnswersVM() { Question = q });
            List<AnswersVM> answerVM = new List<AnswersVM>(qa);

            return View(answerVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveAnswers(List<AnswersVM> answerVMs)
        {

            if (ModelState.IsValid)
            {
                foreach(var i in answerVMs)
                {
                    db.Answers.Add(i.Answers);
                }
                db.SaveChanges();
            }
            return View(db.Questions.ToList());
        }
    }
}