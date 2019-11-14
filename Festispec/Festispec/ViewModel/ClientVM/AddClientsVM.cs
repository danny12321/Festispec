using Festispec.Domain;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Festispec.ViewModel.ClientVM
{
    public class AddClientsVM
    {
        private ClientManageVM _clients;

        public ClientsVM Client { get; set; }

        public ICommand AddClientCommand { get; set; }

        public AddClientsVM(ClientManageVM clients)
        {
            this._clients = clients;
            this.Client = new ClientsVM();

            AddClientCommand = new RelayCommand(AddClientMethod, CanAddClient);
        }

        private void AddClientMethod()
        {
            if (IsMatch())
            {
                _clients.Clients.Add(Client);

                using (var context = new FestispecEntities())
                {
                    context.Clients.Add(Client.ToModel());
                    context.SaveChanges();
                }
                _clients.ShowClientPage();
            }
        }

        public bool CanAddClient()
        {
            if(IsMatch())
            {
                return true;
            }
            return false;
        }
        
        private bool IsMatch()
        {
            if (!IsEmptyField(Client.ClientName) && !IsEmptyField(Client.PostalCode) && !IsEmptyField(Client.Street) && !IsEmptyField(Client.Housenumber) && !IsEmptyField(Client.Country))
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
