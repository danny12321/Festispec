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

        public int Id { get; private set; }

        public QuestionTypeViewModel StringType
        {
            get
            {
                return new QuestionTypeViewModel(_question.Type_questions);
            }
            set
            {
                _question.Type_questions = value.ToModel();
                _question.type_question = value.Id;
                _question.Type_questions = value.ToModel();
                Questionnaires.ChangeType(this);
                SaveChanges();
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

        public virtual Page GetPreviewPage { get => null; }

        public virtual Page GetEditPage { get => null; }

        public QuestionViewModel(QuestionnairesViewModel questionnaires, Questions q)
        {
            Questionnaires = questionnaires;
            _question = q;

            var possible_answers = _question.Possible_answer.ToList()
                .Select(pa => new Possible_answerVM(pa,this));

            Possible_answers = new ObservableCollection<Possible_answerVM>(possible_answers);

            AddAnswerRowCommand = new RelayCommand(AddAnswerRow);
            DeleteQuestionCommand = new RelayCommand(DeleteQuestion);
        }

        public void AddAnswerRow()
        {
            var pa = new Possible_answerVM(this);

            using (var context = new FestispecEntities())
            {
                context.Questions.Attach(_question);
                _question.Possible_answer.Add(pa.ToModel());
                Possible_answers.Add(pa);

                context.SaveChanges();
            }
        }

        public void SaveChanges()
        {
            using (var context = new FestispecEntities())
            {
                context.Entry(_question).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }

        private void DeleteQuestion()
        {
            Possible_answers.ToList().ForEach(pa => pa.Delete());

            using (var context = new FestispecEntities())
            {
                context.Questions.Attach(_question);
                context.Questions.Remove(_question);
                context.SaveChanges();
            }

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
