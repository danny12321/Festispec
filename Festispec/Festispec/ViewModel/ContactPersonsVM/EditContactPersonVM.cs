using Festispec.Domain;
using Festispec.ViewModel.DataService;
using Festispec.ViewModel.FestivalVM;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
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

        public ICommand SaveCommand { get; set; }

        public ContactPersonVM SelectedContactPerson
        {
            get { return _service.SelectedContactPerson; }
        }

        public EditContactPersonVM(IDataService service, ContactPersonManageVM contact)
        {
            _service = service;
            _contact = contact;

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
