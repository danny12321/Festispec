using Festispec.Domain;
using Festispec.ViewModel.DataService;
using Festispec.ViewModel.FestivalVM;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private TypeContactVM _selectedTypeContact;

        public ContactPersonVM ContactPerson { get; set; }

        public ObservableCollection<TypeContactVM> ComboList { get; set; }

        public ICommand SaveCommand { get; set; }

        public TypeContactVM SelectedTypeContact
        {
            get
            {
                return _selectedTypeContact;
            }
            set
            {
                _selectedTypeContact = value;
            }
        }

        public AddContactPersonVM(ContactPersonManageVM contact, IDataService service)
        {
            this._service = service;
            _contact = contact;
            ContactPerson = new ContactPersonVM();

            ContactPerson.ClientId = _service.SelectedClient.ClientId;

            using(var context = new FestispecEntities())
            {
                var typecontacts = context.Type_contacts.ToList().Select(c => new TypeContactVM(c));

                ComboList = new ObservableCollection<TypeContactVM>(typecontacts);
            }

            SaveCommand = new RelayCommand(SaveContact, CanSaveContact);
        }

        private bool CanSaveContact()
        {
            if (IsMatch())
            {
                return true;
            }
            return false;
        }

        private void SaveContact()
        {
            ContactPerson.TypeContact = _selectedTypeContact.Id;
            _contact.ContactPersons.Add(ContactPerson);

            using (var context = new FestispecEntities())
            {
                context.Contactpersons.Add(ContactPerson.ToModel());
                context.SaveChanges();
            }
            _contact.ShowManagementPage();
        }

        private bool IsMatch()
        {
            if (!IsEmptyField(ContactPerson.ContactPersonName) && !IsEmptyField(ContactPerson.ContactPersonLastName))
            {
                return true;
            }
            return false;
        }

        private bool IsEmptyField(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return true;
            }
            return false;
        }

    }
}
