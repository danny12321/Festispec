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

        public int Id { get; private set; }

        private string _type;

        public string Type
        {
            get { return _type; }
            set
            {
                _type = value;

                if (Questionnaires.SelectedQuestion?.Id == Id)
                    Questionnaires.SetQuestionContent();
            }
        }

        public QuestionViewModel(QuestionnairesViewModel questionnaires)
        {
            Questionnaires = questionnaires;
            Type = "Open";
        }
    }
}
