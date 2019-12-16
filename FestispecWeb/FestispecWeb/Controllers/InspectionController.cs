﻿using Festipec.Domain;
using FestispecWeb.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.IO;
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
            var uemail = (string)Session["username"];
            var user = (int)db.Users.Where(u => u.email.Equals(uemail)).Select(u => u.inspector_id).FirstOrDefault();
            var userinspectionid = db.Inspectors_at_inspection.ToList().Where(i => i.inpector_id == user).Select(e => e.inspection_id).ToList();

            
            var inspections = db.Inspections.ToList().Where(i => userinspectionid.Contains(i.id)).ToList();

            return View(inspections);
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

            var uemail = (string)Session["username"];
            var user = (int)db.Users.Where(u => u.email.Equals(uemail)).Select(u => u.inspector_id).FirstOrDefault();
            var userinspectionid = db.Inspectors_at_inspection.ToList().Where(i => i.inpector_id == user).Select(e => e.inspection_id).ToList();

            var questionnaires = db.Questionnaires.ToList().Where(s => (s.inspection_id == id) && (!userinspectionid.Contains((int)s.inspector_id)) && (s.finished == null)).ToList();

            InspectionVM.Questionnaires = questionnaires;
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
                var answers = new List<Answers>();

                if (q.type_question == 4)
                {
                    answers = db.Answers.Where(a => a.question_id == q.id).ToList();
                }
                else
                {
                    var b =  db.Answers.Where(a => a.question_id == q.id).GroupBy(o => o.insertdate).ToList().LastOrDefault();
                    if(b != null)
                    {
                        answers = b.ToList() ;
                    } else
                    {
                        answers.Add(new Answers());
                    }
                }
                answers.ForEach(answer => question.Answers.Add(answer));
                return question;
            });

            List<AnswersVM> answerVM = new List<AnswersVM>(qa);

            return View(answerVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveAnswers(List<AnswersVM> answerVMs)
        {
            var dateNow = DateTime.Now;

            if (ModelState.IsValid)
            {
                foreach (var answerVM in answerVMs)
                {
                    var isDone = answerVMs[0].IsDone;
                    answerVM.Question = db.Questions.FirstOrDefault(q => q.id == answerVM.question_id);
                    if (answerVM.Answers != null)
                        answerVM.Answers.ForEach(answer => { answer.question_id = answerVM.question_id; answer.insertdate = dateNow; });
                    else
                        answerVM.Answers = new List<Answers>();

                    if (answerVM.Question == null) continue;

                    switch (answerVM.Question.type_question)
                    {
                        case 1: // Open vraag
                            SaveOpenQuestion(answerVM);
                            break;

                        case 2: // Multiple choise vraag
                            SaveMultipleQuestion(answerVM);
                            break;

                        case 3: // Select vraag
                            SaveSelectQuestion(answerVM);
                            break;

                        case 4: // Images
                            SaveImageQuestion(answerVM);
                            break;
                    }

                    if(isDone != null)
                    {
                        answerVM.Question.Questionnaires.finished = dateNow;
                    }
                }

                try
                {
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            return Redirect("Inspections");
        }

        private void SaveOpenQuestion(AnswersVM answerVM)
        {
            var Existinganswer = db.Answers.ToList().Where(i => i.question_id == answerVM.question_id).Select(i => i.answer).ToList().LastOrDefault();
            if (answerVM.Answers.Count > 0 && answerVM.Answers[0].answer != null)
            {
                if (Existinganswer == null)
                {
                    db.Answers.Add(answerVM.Answers[0]);
                }
                else
                {
                    if (answerVM.Answers[0].answer != Existinganswer)
                    {
                        db.Answers.Add(answerVM.Answers[0]);
                    }
                }
            }
        }

        private void SaveMultipleQuestion(AnswersVM answerVM)
        {
            var MultipleExistinganswer = db.Answers.ToList().Where(i => i.question_id == answerVM.question_id).Select(i => i.answer).ToList();
            answerVM.Answers.ForEach(answer =>
            {
                if (answer.answer != null)
                {
                    if (MultipleExistinganswer == null)
                    {
                        db.Answers.Add(answer);
                    }
                    else
                    {
                        if (!MultipleExistinganswer.Contains(answer.answer))
                        {
                            db.Answers.Add(answer);
                        }
                    }
                }
            });
        }

        private void SaveSelectQuestion(AnswersVM answerVM)
        {
            var Existinganswer = db.Answers.ToList().Where(i => i.question_id == answerVM.question_id).Select(i => i.answer).ToList().LastOrDefault();
            if (answerVM.Answers.Count > 0 && answerVM.Answers[0].answer != null)
            {
                if (Existinganswer == null)
                {
                    db.Answers.Add(answerVM.Answers[0]);
                }
                else
                {
                    if (answerVM.Answers[0].answer != Existinganswer)
                    {
                        db.Answers.Add(answerVM.Answers[0]);
                    }
                }

            }
        }

        private void SaveImageQuestion(AnswersVM answerVM)
        {
            var dateNow = DateTime.Now;
            foreach (var attachment in answerVM.Attachment)
            {
                if (attachment == null) continue;

                MemoryStream target = new MemoryStream();
                attachment.InputStream.CopyTo(target);
                Byte[] bytes = target.ToArray();
                String file = Convert.ToBase64String(bytes);

                var a = new Answers()
                {
                    question_id = answerVM.Question.id,
                    answer = file,
                    insertdate = dateNow
                };

                db.Answers.Add(a);
            };
        }

    }
}