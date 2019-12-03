using Festispec.Domain;
using Festispec.ViewModel.DataService;
using Festispec.ViewModel.FestivalVM;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Festispec.ViewModel.ContactPersonsVM
{
    public class EditContactPersonVM
    {
        private IDataService _service;
        private ContactPersonManageVM _contact;
        private TypeContactVM _selectedTypeContact;

        public ICommand SaveCommand { get; set; }

        public ObservableCollection<TypeContactVM> ComboList { get; set; }

        public ContactPersonVM SelectedContactPerson
        {
            get { return _service.SelectedContactPerson; }
        }

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

        public EditContactPersonVM(IDataService service, ContactPersonManageVM contact)
        {
            _service = service;
            _contact = contact;

            using (var context = new FestispecEntities())
            {
                var typecontacts = context.Type_contacts.ToList().Select(c => new TypeContactVM(c));

                ComboList = new ObservableCollection<TypeContactVM>(typecontacts);
            }

            SaveCommand = new RelayCommand(SaveChanges, CanSaveChanges);
        }

        private bool CanSaveChanges()
        {
            if (IsMatch())
            {
                return true;
            }
            return false;
        }

        private void SaveChanges()
        {
            SelectedContactPerson.TypeContact = _selectedTypeContact.Id;

            using (var context = new FestispecEntities())
            {
                context.Entry(SelectedContactPerson.ToModel()).State = EntityState.Modified;
                context.SaveChanges();
            }
            _contact.ShowManagementPage();
        }

        private bool IsMatch()
        {
            if (!IsEmptyField(SelectedContactPerson.ContactPersonName) && !IsEmptyField(SelectedContactPerson.ContactPersonLastName))
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
