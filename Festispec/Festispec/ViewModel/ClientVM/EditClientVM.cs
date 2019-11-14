using Festispec.Domain;
using Festispec.ViewModel.DataService;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Festispec.ViewModel.ClientVM
{
    public class EditClientVM
    {
        private IDataService _service;
        private MainViewModel _main;
        private ClientManageVM _clients;

        public ICommand EditClientCommand { get; set; }

        public ClientsVM SelectedClient
        {
            get { return _service.SelectedClient; }
        }

        public EditClientVM(MainViewModel main, IDataService service, ClientManageVM clients)
        {
            this._main = main;
            this._service = service;
            this._clients = clients;

            EditClientCommand = new RelayCommand(SaveClient, CanSaveClient);
        }

        private bool CanSaveClient()
        {
            if (IsMatch())
            {
                return true;
            }
            return false;
        }

        private void SaveClient()
        {
            using (var context = new FestispecEntities())
            {
                context.Entry(SelectedClient.ToModel()).State = EntityState.Modified;
                context.SaveChanges();
            }
            _clients.ShowClientPage();
        }

        private bool IsMatch()
        {
            if (!IsEmptyField(SelectedClient.ClientName) && !IsEmptyField(SelectedClient.PostalCode) && !IsEmptyField(SelectedClient.Street) && !IsEmptyField(SelectedClient.Housenumber) && !IsEmptyField(SelectedClient.Country))
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
