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
            _clients.Clients.Add(Client);

            using (var context = new FestispecEntities())
            {
                context.Clients.Add(Client.ToModel());
                context.SaveChanges();
            }
            _clients.ShowClientPage();
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
            if (IsLetter(Client.ClientName) && IsLetterNumber(Client.PostalCode) && IsLetter(Client.Street) && IsNumber(Client.Housenumber) && IsLetter(Client.Country) && IsPhoneNumber(Client.Phone))
            {
                return true;
            }
            return false;
        }

        private bool IsLetter(string input)
        {
            if (!IsEmptyField(input))
            {
                if (Regex.IsMatch(input, @"^^(?! )[A-Za-z\s]+$"))
                {
                    return true;
                }
            }         
            return false;
        }
        
        private bool IsNumber(string input)
        {
            if (!IsEmptyField(input))
            {
                if (Regex.IsMatch(input, @"^^(?! )[0-9\s]+$"))
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsLetterNumber(string input)
        {
            if (!IsEmptyField(input))
            {
                if (Regex.IsMatch(input, @"^^(?! )[A-Za-z0-9\s]+$"))
                {
                    return true;
                }
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

        private bool IsPhoneNumber(string input)
        {
            if(input == null)
            {
                return true;
            }
            else if (Regex.IsMatch(input, @"^^(?! )[0-9\s]+$"))
            {
                return true;
            }
            return false;
        }
    }
}
