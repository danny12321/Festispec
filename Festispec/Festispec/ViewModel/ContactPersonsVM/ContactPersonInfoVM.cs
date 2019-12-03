using Festispec.Domain;
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
        private string _typeContact;

        public string getTypeContact
        {
            get
            {
                return _typeContact;
            }
            set
            {
                _typeContact = value;
            }
        }

        public ContactPersonVM SelectedContactPerson
        {
            get { return _service.SelectedContactPerson; }
        }

        public ContactPersonInfoVM(IDataService service)
        {
            _service = service;
            getContact();
        }

        public void getContact()
        {
            using(var context = new FestispecEntities())
            {
                 var test = context.Type_contacts.Where(s => s.id == SelectedContactPerson.TypeContact).ToList().Select(s => s.type).FirstOrDefault();
                _typeContact = test;
            }
        }

    }
}
