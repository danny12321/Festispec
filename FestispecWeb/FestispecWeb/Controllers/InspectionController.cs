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

            if (inspection == null)
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

            if (questionaires == null)
            {
                return HttpNotFound();
            }

            var qa = questionaires.Questions.Select(q =>
            {
                var question = new AnswersVM() { Question = q };
                return question;
            });
            List<AnswersVM> answerVM = new List<AnswersVM>(qa);

            return View(answerVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveAnswers(List<AnswersVM> answerVMs)
        {

            if (ModelState.IsValid)
            {
                foreach (var answerVM in answerVMs)
                {
                    answerVM.Question = db.Questions.FirstOrDefault(q => q.id == answerVM.question_id);
                    if (answerVM.Question == null) continue;

                    var question = 1;

                    //switch (Question.type_question)
                    //{
                    //    case 1:
                    //        // Open vraag
                    //        //db.Answers.Add(Answers[0]);
                    //        break;
                    //
                    //    case 2:
                    //        // Multiple choise vraag
                    //        //db.Answers.Add(answerVM.Answers);
                    //        //Answers.ForEach(answer =>
                    //        //{
                    //        //    Console.WriteLine(answer);
                    //        //});
                    //
                    //
                    //        break;
                    //
                    //    case 3:
                    //        // Select vraag
                    //        //db.Answers.Add(Answers[0]);
                    //        break;
                    //
                    //}
                }

                db.SaveChanges();
            }

            return Redirect("/");
        }
    }
}