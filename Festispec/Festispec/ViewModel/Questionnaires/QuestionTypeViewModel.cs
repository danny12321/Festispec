using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Festispec.Domain;

namespace Festispec.ViewModel.Questionnaires
{
    public class QuestionTypeViewModel
    {
        private Type_questions _typeQuestion;

        public QuestionTypeViewModel(Type_questions qt)
        {
            _typeQuestion = qt;
        }

        public int Id
        {
            get
            {
                return _typeQuestion.id;
            }
        }

        public string Type { get
            {
                return _typeQuestion.type;
            }
        }

        internal Type_questions ToModel()
        {
            return _typeQuestion;
        }
    }
}
