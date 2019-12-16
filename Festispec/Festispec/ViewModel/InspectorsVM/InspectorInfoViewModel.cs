using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Festispec.ViewModel.DataService;

namespace Festispec.ViewModel.InspectorsVM
{
    public class InspectorInfoViewModel
    {
        private IDataService _service;

        public InspectorviewModel SelectedInspector
        {
            get { return _service.SelectedInspector; }
        }
        public InspectorInfoViewModel(IDataService dataService)
        {
            this._service = dataService;
        }
    }
}
