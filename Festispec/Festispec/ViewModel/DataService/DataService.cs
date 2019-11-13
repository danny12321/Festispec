using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Festispec.ViewModel.ClientVM;

namespace Festispec.ViewModel.DataService
{
    public class DataService : IDataService
    {
        public ClientsVM SelectedClient { get; set; }
    }
}
