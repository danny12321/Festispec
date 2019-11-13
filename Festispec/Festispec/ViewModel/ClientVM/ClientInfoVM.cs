using Festispec.ViewModel.DataService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel.ClientVM
{
    public class ClientInfoVM
    {
        private IDataService _service;

        public ClientsVM SelectedClient
        {
            get { return _service.SelectedClient; }
        }

        public ClientInfoVM(IDataService service)
        {
            _service = service;
        }

    }
}
