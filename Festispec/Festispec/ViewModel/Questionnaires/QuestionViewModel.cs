using Festispec.Domain;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel.Questionnaires
{
    public class QuestionViewModel
    {
        private QuestionnairesViewModel Questionnaires;

        private Questions _question;

        public int Id { get; private set; }

        private string _type;

        public string Type
        {
            get { return _type; }
            set
            {
                _type = value;
                Questionnaires.SetQuestionContent();
            }
        }

        public string Question
        {
            get
            {
                return _question.question;
            }
            set
            {
                _question.question = value;
            }
        }

        public QuestionViewModel(QuestionnairesViewModel questionnaires, Questions q)
        {
            Questionnaires = questionnaires;
            _question = q;

        }

        public QuestionViewModel(QuestionnairesViewModel questionnaires)
        {
            Questionnaires = questionnaires;
            _question = new Questions() { question = "Nieuwe vraag", type_question = 1 };
        }

        internal Questions ToModel()
        {
            return _question;
        }
    }
}
