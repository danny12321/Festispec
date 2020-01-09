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
    public class TableQuestion : QuestionViewModel, IQuestion
    {
        public TableQuestion(QuestionnairesViewModel questionnaires, Questions q) : base(questionnaires, q)
        {
        }

        public override Page GetEditPage
        {
            get
            {
                var page = new TableEdit();
                page.DataContext = this;
                return page;
            }
        }

        public override Page GetPreviewPage
        {
            get
            {
                var page = new TablePreview();
                page.DataContext = this;
                return page;
            }
        }
    }
}
