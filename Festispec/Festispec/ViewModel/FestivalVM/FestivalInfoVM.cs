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
        private FestivalManagementVM _festivals;
        private MainViewModel _main;

        public ObservableCollection<FestivalVM> Festivals { get; set; }
        public ObservableCollection<ContactPersonVM> Contactpersons { get; set; }

        public ICommand AddContactCommand { get; set; }
        public ICommand ShowContactCommand { get; set; }
        public ICommand EditContactCommand { get; set; }

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

        public FestivalInfoVM(MainViewModel main, IDataService service, FestivalManagementVM festivals)
        {
            this._main = main;
            _service = service;
            this._festivals = festivals;

            using (var context = new FestispecEntities())
            {
                var festival = context.Festivals.ToList()
                             .Select(e => new FestivalVM(e));

                var person = context.Contactpersons.ToList()
                              .Select(i => new ContactPersonVM(i)).Where(i => i.FestivalId == SelectedFestival.FestivalId);

                Festivals = new ObservableCollection<FestivalVM>(festival);
                Contactpersons = new ObservableCollection<ContactPersonVM>(person);
            }
            AddContactCommand = new RelayCommand(ShowAddContactPerson);
            ShowContactCommand = new RelayCommand(ShowContactPersonInfo);
            EditContactCommand = new RelayCommand(ShowEditContactPerson);
        }

        private void ShowEditContactPerson()
        {
            _main.SetPage("ShowEditContactPerson", false);
        }

        private void ShowContactPersonInfo()
        {
            _main.SetPage("ShowContactPersonInfo", false);
        }

        private void ShowAddContactPerson()
        {
            _main.SetPage("ShowAddContactPerson", false);
        }

        public void ShowFestivalPage()
        {
            _main.SetPage("FestivalInfo", false);
        }
    }
}
