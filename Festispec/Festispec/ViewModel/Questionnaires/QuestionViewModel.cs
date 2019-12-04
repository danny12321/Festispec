using Festispec.Domain;
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
    public abstract class QuestionViewModel : IQuestion
    {
        private QuestionnairesViewModel Questionnaires;

        private Questions _question;

        private FestispecEntities _context;

        public int Id { get; private set; }

        private QuestionTypeViewModel _type;

        public QuestionTypeViewModel Type
        {
            get
            {
                return _type;
            }
            set
            {
                _question.Type_questions = value.ToModel();
                _question.type_question = value.Id;
                SaveChanges();
                Questionnaires.ChangeType(this);
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
                SaveChanges();
            }
        }

        public ObservableCollection<Possible_answerVM> Possible_answers { get; set; }

        public ICommand AddAnswerRowCommand { get; set; }

        public ICommand DeleteQuestionCommand { get; set; }

        public ICommand SetTypeCommand { get; set; }

        public virtual Page GetPreviewPage { get => null; }

        public virtual Page GetEditPage { get => null; }

        public QuestionViewModel(QuestionnairesViewModel questionnaires, Questions q, FestispecEntities context)
        {
            Questionnaires = questionnaires;
            _question = q;
            _context = context;

            var possible_answers = _question.Possible_answer.ToList()
                .Select(pa => new Possible_answerVM(pa, this));

            Possible_answers = new ObservableCollection<Possible_answerVM>(possible_answers);

            _type = new QuestionTypeViewModel(q.Type_questions);

            AddAnswerRowCommand = new RelayCommand(AddAnswerRow);
            DeleteQuestionCommand = new RelayCommand(DeleteQuestion);
            SetTypeCommand = new RelayCommand<QuestionTypeViewModel>(SetType);
        }

        private void SetType(QuestionTypeViewModel type)
        {
            Console.WriteLine("Set type");
            Type = type;
        }

        public void AddAnswerRow()
        {
            var pa = new Possible_answerVM(this);

            _context.Questions.Attach(_question);
            _question.Possible_answer.Add(pa.ToModel());
            Possible_answers.Add(pa);

            _context.SaveChanges();
        }

        public void SaveChanges()
        {
            _context.Entry(_question).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }

        private void DeleteQuestion()
        {
            Possible_answers.ToList().ForEach(pa => pa.Delete());

            _context.Questions.Attach(_question);
            _context.Questions.Remove(_question);
            _context.SaveChanges();


            Questionnaires.Questions.Remove(this);
            Questionnaires.SelectedQuestion = null;
            Console.WriteLine("Selected question null");
        }

        internal Questions ToModel()
        {
            return _question;
        }
    }
}
