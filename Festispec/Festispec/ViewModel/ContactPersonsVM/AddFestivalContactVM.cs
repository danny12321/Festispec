using Festispec.Domain;
using Festispec.ViewModel.ClientVM;
using Festispec.ViewModel.DataService;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Festispec.ViewModel.ContactPersonsVM
{
    public class AddFestivalContactVM : ViewModelBase
    {
        private MainViewModel _main;
        private IDataService _service;

        public ObservableCollection<ContactPersonVM> ContactPersons { get; set; }


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

        public AddFestivalContactVM(MainViewModel main, IDataService data)
        {
            _service = data;
            _main = main;

            using (var context = new FestispecEntities())
            {
                var contacts = context.Contactpersons.ToList()
                             .Select(contact => new ContactPersonVM(contact)).Where(e => e.ClientId.Equals(SelectedClient.ClientId));

                ContactPersons = new ObservableCollection<ContactPersonVM>(contacts);
            }
        }
    }
}
