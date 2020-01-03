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
using System.Data.Entity;
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

        public int Id
        {
            get
            {
                return Questionnaire.id;
            }
        }

        public string Name
        {
            get { return Questionnaire.name; }
            set { Questionnaire.name = value; SaveChanges(); }
        }

        public ObservableCollection<QuestionTypeViewModel> QuestionTypes { get; set; }

        public ObservableCollection<QuestionViewModel> Questions { get; set; }

        public ObservableCollection<Domain.Questionnaires> Templates { get; set; }

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

        private bool _templateDialogOpen;

        public bool TemplateDialogOpen { get { return _templateDialogOpen; } set { _templateDialogOpen = value; RaisePropertyChanged("TemplateDialogOpen"); } }

        public ICommand AddQuestionCommand { get; set; }

        public ICommand SelectTemplateCommand { get; set; }

        public ICommand OpenTemplateDialogCommand { get; set; }

        public QuestionnairesViewModel(Domain.Questionnaires questionnaire)
        {
            Init(questionnaire);
        }

        public QuestionnairesViewModel(IDataService dataService)
        {
            this.dataService = dataService;
            Init(dataService.SelectedQuestionnaire.Questionnaire);
        }

        private void Init(Domain.Questionnaires questionnaire)
        {
            Questionnaire = questionnaire;
            AddQuestionCommand = new RelayCommand(AddQuestion);
            SelectTemplateCommand = new RelayCommand<Domain.Questionnaires>(SelectTemplate);
            OpenTemplateDialogCommand = new RelayCommand(() => TemplateDialogOpen = true);


            using (var context = new FestispecEntities())
            {
                var templates = context.Questionnaires.Where(q => q.inspection_id == null).ToList();
                Templates = new ObservableCollection<Domain.Questionnaires>(templates);

                var questions = Questionnaire.Questions.ToList()
                    .Select(q => GetQuestionClass(q));

                Questions = new ObservableCollection<QuestionViewModel>(questions);

                var questionTypes = context.Type_questions.ToList()
                    .Select(qt => new QuestionTypeViewModel(qt));

                QuestionTypes = new ObservableCollection<QuestionTypeViewModel>(questionTypes);
            }
        }

        private QuestionViewModel GetQuestionClass(Questions q)
        {
            switch (q.type_question)
            {
                case 1:
                    return new OpenQuestion(this, q);

                case 2:
                    return new MultipleChoiseQuestion(this, q);

                case 3:
                    return new SelectQuestion(this, q);

                case 4:
                    return new ImageQuestion(this, q);

                case 5:
                    return new TableQuestion(this, q);

                default:
                    return null;
            }
        }

        private void AddQuestion()
        {
            var question = new Questions() { question = "Nieuwe vraag", type_question = 2, questionnaire_id = Questionnaire.id };

            using (var context = new FestispecEntities())
            {
                question.Type_questions = context.Type_questions.First(tq => tq.id == question.type_question);
                context.Questions.Add(question);

                context.SaveChanges();
            }

            var newQuestion = GetQuestionClass(question);
            SelectedQuestion = newQuestion;
            Questions.Add(newQuestion);
        }

        private void SaveChanges()
        {
            using (var context = new FestispecEntities())
            {
                context.Entry(Questionnaire).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }

        private void SelectTemplate(Domain.Questionnaires template)
        {
            Questions.Clear();

            using (var context = new FestispecEntities())
            {
                context.Questions.RemoveRange(context.Questions.Where(q => q.questionnaire_id == Questionnaire.id).ToList());

                var questions = context.Questions.AsNoTracking().Include(q => q.Possible_answer).Where(q => q.questionnaire_id == template.id).ToList();
                var pAnswers = new List<Domain.Possible_answer>();

                questions.ForEach(q =>
                {
                    q.questionnaire_id = Questionnaire.id;
                });

                context.Questions.AddRange(questions);
                context.SaveChanges();


                questions.ForEach(q =>
                {
                    Questions.Add(GetQuestionClass(q));
                });
            }

            TemplateDialogOpen = false;
        }
    }
}
