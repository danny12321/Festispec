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
        public IEnumerable<Questions> Questions { get; set; }
        public Answers Answers { get; set; }
        public IEnumerable<Questionnaires> Questionnaires { get; set; }

        public AnswersVM()
        {
            Questions = new ObservableCollection<Questions>();
            Questionnaires = new ObservableCollection<Questionnaires>();
            Answers = new Answers();
        }
        
    }
}