using Festispec.Domain;
using Festispec.ViewModel.ClientVM;
using Festispec.ViewModel.DataService;
using GalaSoft.MvvmLight;
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
    public class ContactPersonManageVM : ViewModelBase
    {
        private MainViewModel _main;
        private IDataService _service;

        public ObservableCollection<ContactPersonVM> ContactPersons { get; set; }

        public ICommand showAddContactPerson { get; set; }
        public ICommand showEditContactPerson { get; set; }
        public ICommand ShowContactPerson { get; set; }

        public ClientsVM SelectedClient
        {
            get { return _service.SelectedClient; }
        }

        public ContactPersonVM SelectedContactPerson
        {
            get { return _service.SelectedContactPerson; }
            set 
            { 
                _service.SelectedContactPerson = value;
                RaisePropertyChanged();
            }
        }

        public ContactPersonManageVM(MainViewModel main, IDataService service)
        {
            _main = main;
            _service = service;

            using (var context = new FestispecEntities())
            {
                var contacts = context.Contactpersons.ToList()
                             .Select(contact => new ContactPersonVM(contact)).Where(e => e.ClientId.Equals(SelectedClient.ClientId));

                ContactPersons = new ObservableCollection<ContactPersonVM>(contacts);
            }

            showAddContactPerson = new RelayCommand(ShowAddContactPerson);
            showEditContactPerson = new RelayCommand(ShowEditContactPerson);
            ShowContactPerson = new RelayCommand(ShowContactPersonInfo);
        }

        private void ShowAddContactPerson()
        {
            _main.SetPage("ShowAddContactPerson");
        }

        public void ShowManagementPage()
        {
            _main.SetPage("ContactPersonManagement");
        }

        private void ShowEditContactPerson()
        {
            _main.SetPage("ShowEditContactPerson");
        }
        private void ShowContactPersonInfo()
        {
            _main.SetPage("ShowContactPersonInfo");
        }
    }
}
