using Festispec.Domain;
using Festispec.ViewModel.DataService;
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

namespace Festispec.ViewModel.ClientVM
{
    public class ClientInfoVM : ViewModelBase
    {
        private IDataService _service;
        private ClientManageVM _clients;
        private MainViewModel _main;

        public ObservableCollection<FestivalVM.FestivalVM> Festivals { get; set; }

        public ICommand AddFestivalCommand { get; set; }
        public ICommand ShowEditCommand { get; set; }

        public ClientsVM SelectedClient
        {
            get { return _service.SelectedClient; }
        }

        public FestivalVM.FestivalVM SelectedFestival
        {
            get { return _service.SelectedFestival; }
            set
            {
                _service.SelectedFestival = value;
                RaisePropertyChanged();
            }
        }

        public ClientInfoVM(MainViewModel main, IDataService service, ClientManageVM clients)
        {
            this._main = main;
            _service = service;
            this._clients = clients;

            using (var context = new FestispecEntities())
            {
                var festival = context.Festivals.ToList()
                             .Select(e => new FestivalVM.FestivalVM(e)).Where(e => e.ClientId.Equals(SelectedClient.ClientId));

                Festivals = new ObservableCollection<FestivalVM.FestivalVM>(festival);
            }

            AddFestivalCommand = new RelayCommand(ShowAddFestival);
            ShowEditCommand = new RelayCommand(ShowEditFestival);
        }

        private void ShowEditFestival()
        {
            _main.SetPage("EditFestival");
        }

        public void ShowAddFestival()
        {
            _main.SetPage("addFestival");
        }

        public void ShowClientPage()
        {
            _main.SetPage("ClientInfo");
        }

    }
}
