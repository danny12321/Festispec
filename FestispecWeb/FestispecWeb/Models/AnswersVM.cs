using Festipec.Domain;
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

        public Answers Answers { get; set; }     
        
        public AnswersVM()
        {
            Question = new Questions();
            Answers = new Answers();
        }
    }
}