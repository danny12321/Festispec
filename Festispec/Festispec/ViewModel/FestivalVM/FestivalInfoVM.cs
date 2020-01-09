using Festispec.Domain;
using Festispec.ViewModel.ContactPersonsVM;
using Festispec.ViewModel.DataService;
using Festispec.ViewModel.FestivalVMs;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Festispec.ViewModel.FestivalVM
{
    public class FestivalInfoVM : ViewModelBase
    {
        private IDataService _service;
        private MainViewModel _main;
        public ICommand ShowQuotations { get; set; }
        public ObservableCollection<ContactPersonVM> Contactpersons { get; set; }

        public ContactPersonVM Contact;

        public ICommand ShowContactCommand { get; set; }
        public ICommand RemoveContactPersonToFestival { get; set; }

        public FestivalVM SelectedFestival
        {
            get { return _service.SelectedFestival; }
            set
            {
                _service.SelectedFestival = value;
                RaisePropertyChanged();
            }
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

        public FestivalInfoVM(MainViewModel main, IDataService service, FestivalManagementVM festival)
        {
            this._main = main;
            _service = service;

            using (var context = new FestispecEntities())
            {
                context.Festivals.Attach(SelectedFestival.ToModel());
                Contactpersons = new ObservableCollection<ContactPersonVM>(SelectedFestival.ContactPersons);
            }
            ShowQuotations = new RelayCommand(QuotationsList);
            ShowContactCommand = new RelayCommand(ShowContact);
            RemoveContactPersonToFestival = new RelayCommand(RemoveContactPerson, CanRemoveContactPerson);
        }
        private void QuotationsList()
        {
            _main.SetPage("ShowQuotations");
        }
        private bool CanRemoveContactPerson()
        {
            if(SelectedContactPerson != null)
            {
                return true;
            }
            return false;
        }

        private void RemoveContactPerson()
        {
            using(var context = new FestispecEntities())
            {
                context.Festivals.Attach(SelectedFestival.ToModel());
                SelectedFestival.ToModel().Contactpersons.Remove(SelectedContactPerson.ToModel());
                Contactpersons.Remove(SelectedContactPerson);
                context.SaveChanges();

            }
            base.RaisePropertyChanged();
        }

        private void ShowContact()
        {
            _main.SetPage("AddContactFestival");
        }
    }
}
