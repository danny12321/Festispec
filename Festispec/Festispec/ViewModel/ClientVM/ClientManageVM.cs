using Festispec.Domain;
using Festispec.View.ClientsViews;
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

        public ObservableCollection<ClientsVM> Clients { get; set; }

        public ICommand showAddClient { get; set; }

        public ClientManageVM(MainViewModel main)
        {
            _main = main;

            using (var context = new FestispecEntities())
            {
                var clients = context.Clients.ToList()
                             .Select(client => new ClientsVM(client));

                Clients = new ObservableCollection<ClientsVM>(clients);
            }

            showAddClient = new RelayCommand(ShowAddPage);
        }

        private void ShowAddPage()
        {
            _main.SetPage("AddClient");
        }
    }
}
