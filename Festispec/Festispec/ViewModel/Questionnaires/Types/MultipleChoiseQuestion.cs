using Festispec.Domain;
using Festispec.View.Questionnaires.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Festispec.ViewModel.Questionnaires.Types
{
    public class MultipleChoiseQuestion : QuestionViewModel, IQuestion
    {
        public MultipleChoiseQuestion(QuestionnairesViewModel questionnaires, Questions q, FestispecEntities context) : base(questionnaires, q, context)
        {
        }

        public override Page GetPreviewPage
        {
            get
            {
                var page = new MultipleChoisePreview();
                page.DataContext = this;
                return page;
            }
        }

        public override Page GetEditPage
        {
            get
            {
                var page = new MultipleChoiseEdit();
                page.DataContext = this;
                return page;
            }
        }
    }
}
