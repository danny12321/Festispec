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
    public class AddFestivalContactVM : ViewModelBase
    {
        private MainViewModel _main;
        private IDataService _service;

        public ObservableCollection<ContactPersonVM> ContactPersons { get; set; }

        public ICommand AddContactPersonToFestival { get; set; }

        public ClientsVM SelectedClient
        {
            get { return _service.SelectedClient; }
        }

        public FestivalVM.FestivalVM SelectedFestival
        {
            get { return _service.SelectedFestival; }
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
                ContactPersons = new ObservableCollection<ContactPersonVM>();

                context.Festivals.Attach(SelectedFestival.ToModel());
                context.Contactpersons.Where(s => s.client_id == SelectedClient.ClientId).ToList().Where(s => !s.Festivals.Contains(SelectedFestival.ToModel())).ToList().ForEach(s => ContactPersons.Add(new ContactPersonVM(s)));
            }

            AddContactPersonToFestival = new RelayCommand(AddContactPerson, CanAddContact);
        }

        private bool CanAddContact()
        {
            return true;
        }

        private void AddContactPerson()
        {
            if(SelectedContactPerson != null)
            {
                using (var context = new FestispecEntities())
                {
                    context.Contactpersons.Attach(SelectedContactPerson.ToModel());
                    context.Festivals.Attach(SelectedFestival.ToModel());

                    SelectedFestival.ContactPersons.Add(SelectedContactPerson);
                    SelectedContactPerson.ToModel().Festivals.Add(SelectedFestival.ToModel());

                    ContactPersons.Remove(SelectedContactPerson);

                    context.SaveChanges();
                }
                base.RaisePropertyChanged();

                _main.SetPage("FestivalInfo", false);
            }
        }
    }
}
