using Festispec.Domain;
using Festispec.View.Questionnaires;
using Festispec.ViewModel.DataService;
using Festispec.ViewModel.Inspections;
using Festispec.ViewModel.Questionnaires.Types;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Ioc;
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
        public Domain.Questionnaires Questionnaire;

        public int Id { get
            {
                return Questionnaire.id;
            } }

        private InspectionVM _inspection;

        private FestispecEntities _context;

        public ObservableCollection<QuestionTypeViewModel> QuestionTypes { get; set; }

        public ObservableCollection<QuestionViewModel> Questions { get; set; }

        private QuestionViewModel _selectedQuestion;

        public QuestionViewModel SelectedQuestion
        {
            get { return _selectedQuestion; }
            set
            {
                _selectedQuestion = value;
                EditPage = _selectedQuestion?.GetEditPage;
                RaisePropertyChanged("EditPage");
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

        public void ChangeType(QuestionViewModel question)
        {
            var newVm = GetQuestionClass(question.ToModel());
            int index = Questions.IndexOf(question);
            Questions[index] = newVm;
            SelectedQuestion = newVm;

            RaisePropertyChanged("EditPage");
        }

        private Page _editPage;
        private IDataService dataService;

        public Page EditPage
        {
            get { return _editPage; }
            set
            {
                _editPage = value;
                RaisePropertyChanged("EditPage");
            }
        }

        public ICommand AddQuestionCommand { get; set; }


        public QuestionnairesViewModel(Inspections.InspectionVM inspection, FestispecEntities context)
        {
            _inspection = inspection;
            _context = context;

            // TODO: make inspector id nullable
            Questionnaire = new Domain.Questionnaires() { inspection_id = inspection.Id };


            context.Questionnaires.Add(Questionnaire);
            context.SaveChanges();

            var questionTypes = context.Type_questions.ToList()
                .Select(qt => new QuestionTypeViewModel(qt));

            QuestionTypes = new ObservableCollection<QuestionTypeViewModel>(questionTypes);
            Questions = new ObservableCollection<QuestionViewModel>();

        }

        public QuestionnairesViewModel(Domain.Questionnaires questionnaire, FestispecEntities context)
        {
            _context = context;
            Init(questionnaire);
        }

        [PreferredConstructor]
        public QuestionnairesViewModel(IDataService dataService, FestispecEntities context)
        {
            _context = context;
            this.dataService = dataService;
            Init(dataService.SelectedQuestionnaire.Questionnaire);
        }

        private void Init(Domain.Questionnaires questionnaire)
        {
            Console.WriteLine("INIT " + questionnaire.id);
            Questionnaire = questionnaire;
            AddQuestionCommand = new RelayCommand(AddQuestion);

            var questions = questionnaire.Questions.ToList()
                .Select(q => GetQuestionClass(q));

            Questions = new ObservableCollection<QuestionViewModel>(questions);

            var questionTypes = _context.Type_questions.ToList()
                .Select(qt => new QuestionTypeViewModel(qt));

            QuestionTypes = new ObservableCollection<QuestionTypeViewModel>(questionTypes);
        }

        private QuestionViewModel GetQuestionClass(Questions q)
        {
            switch (q.type_question)
            {
                case 1:
                    return new OpenQuestion(this, q, _context);

                case 2:
                    return new MultipleChoiseQuestion(this, q, _context);

                case 3:
                    return new SelectQuestion(this, q, _context);

                default:
                    return null;
            }
        }

        private void AddQuestion()
        {
            var question = new Questions() { question = "New question", type_question = 2, questionnaire_id = Questionnaire.id };

            question.Type_questions = _context.Type_questions.First(tq => tq.id == question.type_question);
            _context.Questions.Add(question);

            _context.SaveChanges();

            var newQuestion = GetQuestionClass(question);
            SelectedQuestion = newQuestion;
            Questions.Add(newQuestion);
        }

    }
}
