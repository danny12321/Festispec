using Festispec.Domain;
using Festispec.ViewModel.DataService;
using Festispec.ViewModel.FestivalVM;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Festispec.ViewModel.ContactPersonsVM
{
    public class AddContactPersonVM
    {
        private ContactPersonManageVM _contact;
        private IDataService _service;

        public ContactPersonVM ContactPerson { get; set; }

        public ICommand SaveCommand { get; set; }

        public AddContactPersonVM(ContactPersonManageVM contact, IDataService service)
        {
            this._service = service;
            _contact = contact;
            ContactPerson = new ContactPersonVM();

            ContactPerson.ClientId = _service.SelectedClient.ClientId;
/*            ContactPerson.ToModel().Clients = _service.SelectedClient.ToModel();
            _service.SelectedClient.ToModel().Contactpersons.Add(ContactPerson.ToModel());*/

            SaveCommand = new RelayCommand(SaveContact, CanSaveContact);
        }

        private bool CanSaveContact()
        {
            return true;
        }

        private void SaveContact()
        {
            _contact.ContactPersons.Add(ContactPerson);

            using (var context = new FestispecEntities())
            {
                context.Contactpersons.Add(ContactPerson.ToModel());
                context.SaveChanges();
            }
            _contact.ShowManagementPage();
        }
    }
}
