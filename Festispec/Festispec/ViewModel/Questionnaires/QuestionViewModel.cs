using Festispec.Domain;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Festispec.ViewModel.Questionnaires
{
    public class QuestionViewModel
    {
        private QuestionnairesViewModel Questionnaires;

        private Questions _question;

        public int Id { get; private set; }

        public string StringType
        {
            get
            {
                return _question.Type_questions?.type;
            }
            set
            {
                Type = Questionnaires.QuestionTypes.ToList().Where(t => t.Id == int.Parse(value)).First().ToModel();
                Questionnaires.RaisePropertyChanged("GetAnswerRowsVisibility");
            }
        }

        public Type_questions Type
        {
            get
            {
                return _question.Type_questions;
            }
            set
            {
                _question.Type_questions = value;
                _question.type_question = value.id;
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

        public ObservableCollection<Possible_answerVM> Possible_answers { get; set; }

        public ICommand AddAnswerRowCommand { get; set; }

        public QuestionViewModel(QuestionnairesViewModel questionnaires, Questions q)
        {
            Questionnaires = questionnaires;
            _question = q;

            var possible_answers = _question.Possible_answer.ToList()
                .Select(pa => new Possible_answerVM(pa));

            Possible_answers = new ObservableCollection<Possible_answerVM>(possible_answers);

            AddAnswerRowCommand = new RelayCommand(AddAnswerRow);
        }

        public QuestionViewModel(QuestionnairesViewModel questionnaires)
        {
            Questionnaires = questionnaires;
            _question = new Questions() { type_question = 1 };

            Possible_answers = new ObservableCollection<Possible_answerVM>();

            AddAnswerRowCommand = new RelayCommand(AddAnswerRow);
        }

        public void AddAnswerRow()
        {
            Possible_answers.Add(new Possible_answerVM());
        }

        internal Questions ToModel()
        {
            return _question;
        }
    }
}
