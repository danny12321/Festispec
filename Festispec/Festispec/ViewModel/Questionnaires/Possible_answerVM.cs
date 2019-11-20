using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Festispec.Domain;
using GalaSoft.MvvmLight.CommandWpf;

namespace Festispec.ViewModel.Questionnaires
{
    public class Possible_answerVM
    {
        private Possible_answer _possibleAnswer;

        private QuestionViewModel _question;

        public string Answer {
            get
            {
                return _possibleAnswer.answer;
            }
            set
            {
                _possibleAnswer.answer = value;
                SaveChanges();
            }
        }

        public ICommand DeleteCommand { get; set; }

        public Possible_answerVM(Possible_answer pa, QuestionViewModel question)
        {
            _possibleAnswer = pa;
            _question = question;

            DeleteCommand = new RelayCommand(Delete);
        }

        public Possible_answerVM(QuestionViewModel question)
        {
            _possibleAnswer = new Possible_answer() { question_id = question.Id, answer = "" };
            _question = question;

            DeleteCommand = new RelayCommand(Delete);
        }

        internal Possible_answer ToModel()
        {
            return _possibleAnswer;
        }

        public void Delete()
        {
            using (var context = new FestispecEntities())
            {
                context.Possible_answer.Attach(_possibleAnswer);
                context.Possible_answer.Remove(_possibleAnswer);
                _question.Possible_answers.Remove(this);
                context.SaveChanges();
            }
        }

        private void SaveChanges()
        {
            using (var context = new FestispecEntities())
            {
                context.Entry(_possibleAnswer).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }

    }
}
