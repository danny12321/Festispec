using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Festispec.Domain;

namespace Festispec.ViewModel.Questionnaires
{
    public class Possible_answerVM
    {
        private Possible_answer _possibleAnswer;

        public string Answer {
            get
            {
                return _possibleAnswer.answer;
            }
            set
            {
                _possibleAnswer.answer = value;
            }
        }



        public Possible_answerVM(Possible_answer pa)
        {
            _possibleAnswer = pa;
        }

        public Possible_answerVM()
        {
            _possibleAnswer = new Possible_answer();
        }

    }
}
