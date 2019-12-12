using Festispec.ViewModel.ClientVM;
using Festispec.ViewModel.FestivalVM;
using Festispec.ViewModel.ContactPersonsVM;
using Festispec.ViewModel.Inspections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Festispec.ViewModel.QuotationsVM;

namespace Festispec.ViewModel.DataService
{
    public interface IDataService
    {
        ClientsVM SelectedClient { get; set; }

        FestivalVM.FestivalVM SelectedFestival { get; set; }

        ContactPersonVM SelectedContactPerson { get; set; }
        InspectionVM SelectedInspection { get; set; }

       QuotationViewModel SelectedQuotation { get; set; }
    }
}
