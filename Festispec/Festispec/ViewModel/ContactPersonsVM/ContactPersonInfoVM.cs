using Festispec.ViewModel.DataService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel.ContactPersonsVM
{
    public class ContactPersonInfoVM
    {
        private IDataService _service;

        public ContactPersonVM SelectedContactPerson
        {
            get { return _service.SelectedContactPerson; }
        }

        public ContactPersonInfoVM(IDataService service)
        {
            _service = service;
        }
    }
}
