using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Festispec.ViewModel.ClientVM;
using Festispec.ViewModel.ContactPersonsVM;
using Festispec.ViewModel.FestivalVM;
using Festispec.ViewModel.Inspections;
using Festispec.ViewModel.InspectorsVM;

namespace Festispec.ViewModel.DataService
{
    public class DataService : IDataService
    {
        public ClientsVM SelectedClient { get; set; }
        public FestivalVM.FestivalVM SelectedFestival { get; set; }
        public ContactPersonVM SelectedContactPerson { get; set; }
        public InspectionVM SelectedInspection { get; set; }

        public InspectorviewModel SelectedInspector { get; set; }
    }
}
