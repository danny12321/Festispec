using Festispec.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace FestispecWeb.Models
{
    public class AnswersVM
    {
        public Questions Question { get; set; }

        public List<Answers> Answers { get; set; } = new List<Answers>();

        public List<HttpPostedFileBase> Attachment { get; set; }

        public int question_id { get; set; }

        public string IsDone { get; set; }
    }
}