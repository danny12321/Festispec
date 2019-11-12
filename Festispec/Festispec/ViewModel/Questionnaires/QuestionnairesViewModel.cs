using Festispec.Domain;
using Festispec.View.Questionnaires;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
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

        private Page _questionContent;

        public Page QuestionContent
        {
            get { return _questionContent; }
            set
            {
                _questionContent = value;
                RaisePropertyChanged("QuestionContent");
            }
        }

        private QuestionViewModel _selectedQuestion;

        public QuestionViewModel SelectedQuestion
        {
            get { return _selectedQuestion; }
            set
            {
                _selectedQuestion = value;
                RaisePropertyChanged("SelectedQuestion");
                RaisePropertyChanged("GetVisibility");
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

        public ICommand SelectQuestionCommand { get; set; }

        public ICommand AddQuestionCommand { get; set; }

        public QuestionnairesViewModel()
        {
            SelectQuestionCommand = new RelayCommand<int>(SelectQuestion);
            AddQuestionCommand = new RelayCommand(AddQuestion);

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

        private void SelectQuestion(int id)
        {
            var question = Questions.Where(e => e.Id == id).First();

            if (question != null)
            {
                SelectedQuestion = question;
                SetQuestionContent();
            }
        }

        private void AddQuestion()
        {
            SelectedQuestion = new QuestionViewModel(this);
            SetQuestionContent();
        }

        public void SetQuestionContent()
        {

            switch (SelectedQuestion.Type)
            {
                case "ComboBox":
                    QuestionContent = new SelectBox();
                    break;

                case "Open":
                    QuestionContent = new Open();
                    break;

                default:
                    QuestionContent = null;
                    break;
            }
        }
    }
}
