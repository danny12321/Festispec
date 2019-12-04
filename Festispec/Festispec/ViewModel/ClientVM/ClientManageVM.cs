using Festispec.Domain;
using Festispec.View.ClientsViews;
using Festispec.ViewModel.DataService;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Festispec.ViewModel.ClientVM
{
    public class ClientManageVM : ViewModelBase
    {
        private MainViewModel _main;
        private IDataService _service;

        public ObservableCollection<ClientsVM> Clients { get; set; }

        public ICommand showAddClient { get; set; }
        public ICommand ShowClientInfo { get; set; }
        public ICommand ShowEditClient { get; set; }
        public ICommand ShowContactPerson { get; set; }

        public ClientsVM SelectedClient
        {
            get { return _service.SelectedClient; }
            set
            {
                _service.SelectedClient = value;
                RaisePropertyChanged();
            }
        }

        public ClientManageVM(MainViewModel main, IDataService service)
        {
            _main = main;
            _service = service;

            using (var context = new FestispecEntities())
            {
                var clients = context.Clients.Include("Contactpersons").ToList()
                             .Select(client => new ClientsVM(client));

                Clients = new ObservableCollection<ClientsVM>(clients);
            }

            showAddClient = new RelayCommand(ShowAddPage);
            ShowClientInfo = new RelayCommand(showClient);
            ShowEditClient = new RelayCommand(ShowEditPage);
            ShowContactPerson = new RelayCommand(ShowContactPage);
        }

        private void ShowContactPage()
        {
            _main.SetPage("ContactPersonManagement", false);
        }

        private void ShowEditPage()
        {
            _main.SetPage("EditClient", false);
        }

        private void showClient()
        {
            _main.SetPage("ClientInfo", false);
        }

        private void ShowAddPage()
        {
            _main.SetPage("AddClient", false);
        }

        public void ShowClientPage()
        {
            _main.SetPage("Clients", false);
        }
    }
}
