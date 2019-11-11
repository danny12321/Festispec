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

        public QuestionViewModel SelectedQuestion { get; set; }

        public ICommand SelectQuestionCommand { get; set; }

        public ICommand AddQuestionCommand { get; set; }

        public QuestionnairesViewModel()
        {
            SelectQuestionCommand = new RelayCommand<int>(SelectQuestion);
            AddQuestionCommand = new RelayCommand(AddQuestion);

            Questions = new ObservableCollection<QuestionViewModel>();

            Questions.Add(new QuestionViewModel(this));
            Questions.Add(new QuestionViewModel(this));
            Questions.Add(new QuestionViewModel(this));


            QuestionTypes = new ObservableCollection<QuestionTypeViewModel>();

            QuestionTypes.Add(new QuestionTypeViewModel() { Type = "ComboBox" });
            QuestionTypes.Add(new QuestionTypeViewModel() { Type = "Open" });
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
        }

        public void SetQuestionContent()
        {
            Console.WriteLine("Set content");

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
