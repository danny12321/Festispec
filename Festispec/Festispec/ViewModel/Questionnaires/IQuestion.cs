using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Festispec.ViewModel.Questionnaires
{
    public interface IQuestion
    {
        Page GetPreviewPage { get; }

        Page GetEditPage { get; }


    }
}
