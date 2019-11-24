using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Festispec.ViewModel.ClientVM;
using Festispec.ViewModel.ContactPersonsVM;
using Festispec.ViewModel.FestivalVM;

namespace Festispec.ViewModel.DataService
{
    public class DataService : IDataService
    {
        public ClientsVM SelectedClient { get; set; }
        public FestivalVM.FestivalVM SelectedFestival { get; set; }
        public ContactPersonVM SelectedContactPerson { get; set; }
    }
}
