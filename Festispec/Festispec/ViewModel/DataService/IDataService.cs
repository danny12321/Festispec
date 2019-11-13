using Festispec.ViewModel.ClientVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel.DataService
{
    public interface IDataService
    {
        ClientsVM SelectedClient { get; set; }
    }
}
