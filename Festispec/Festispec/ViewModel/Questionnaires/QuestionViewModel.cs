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
                Console.WriteLine(value);
                _question.Type_questions = value.ToModel();
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
                //_question.Type_questions = value;
                //_question.type_question = value.id;
                Questionnaires.ChangeType(this, value);
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

        public virtual Page GetPreviewPage { get => null; }

        public virtual Page GetEditPage { get => null; }

        public QuestionViewModel(QuestionnairesViewModel questionnaires, Questions q)
        {
            Questionnaires = questionnaires;
            _question = q;

            var possible_answers = _question.Possible_answer.ToList()
                .Select(pa => new Possible_answerVM(pa));

            Possible_answers = new ObservableCollection<Possible_answerVM>(possible_answers);

            AddAnswerRowCommand = new RelayCommand(AddAnswerRow);

            
        }

        public void AddAnswerRow()
        {
            Possible_answers.Add(new Possible_answerVM());
        }

        public void SaveChanges()
        {
            using (var context = new FestispecEntities())
            {
                var question = context.Questions.First(q => q.id == _question.id);
                question.question = Question;
                context.SaveChanges();
            }
        }

        internal Questions ToModel()
        {
            return _question;
        }
    }
}
