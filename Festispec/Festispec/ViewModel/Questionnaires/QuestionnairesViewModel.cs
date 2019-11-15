using Festispec.Domain;
using Festispec.View.Questionnaires;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Festispec.ViewModel.Questionnaires
{
    public class QuestionnairesViewModel : ViewModelBase
    {
        public ObservableCollection<QuestionTypeViewModel> QuestionTypes { get; set; }

        public ObservableCollection<QuestionViewModel> Questions { get; set; }

        private QuestionViewModel _selectedQuestion;

        public QuestionViewModel SelectedQuestion
        {
            get { return _selectedQuestion; }
            set
            {
                _selectedQuestion = value;
                RaisePropertyChanged("SelectedQuestion");
                RaisePropertyChanged("GetVisibility");
                RaisePropertyChanged("GetAnswerRowsVisibility");
            }
        }

        public string GetVisibility
        {
            get
            {
                if (SelectedQuestion == null) return "Hidden";
                else return "Visible";
            }
        }

        public string GetAnswerRowsVisibility
        {
            get
            {
                switch (SelectedQuestion?.Type?.type)
                {
                    case "ComboBox":
                        return "Visible";
                    default:
                        return "Hidden";
                }
            }
        }

        public ICommand AddQuestionCommand { get; set; }

        public ICommand AddAnswerRowCommand { get; set; }

        public QuestionnairesViewModel()
        {
            AddQuestionCommand = new RelayCommand(AddQuestion);
            AddAnswerRowCommand = new RelayCommand(AddAnswerRow);

            using (var context = new FestispecEntities())
            {
                var questions = context.Questions.ToList()
                    .Select(q => new QuestionViewModel(this, q));


                Questions = new ObservableCollection<QuestionViewModel>(questions);

                var questionTypes = context.Type_questions.ToList()
                    .Select(qt => new QuestionTypeViewModel(qt));

                QuestionTypes = new ObservableCollection<QuestionTypeViewModel>(questionTypes);
            }
        }

        private void AddQuestion()
        {
            var newQuestion = new QuestionViewModel(this);
            SelectedQuestion = newQuestion;
            Questions.Add(SelectedQuestion);
        }

        private void AddAnswerRow()
        {
            SelectedQuestion?.AddAnswerRow();
        }
    }
}
