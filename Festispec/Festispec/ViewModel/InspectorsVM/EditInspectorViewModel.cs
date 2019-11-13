using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel.InspectorsVM
{
    class EditInspectorViewModel
    {
        private InspectorviewModel _selectedInspector;
        public EditInspectorViewModel(InspectorviewModel i)
        {
            _selectedInspector = i;
        }
    }
}
