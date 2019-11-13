using Festispec.Domain;
using Festispec.ViewModel.DataService;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Festispec.ViewModel.ClientVM
{
    public class ClientInfoVM
    {
        private IDataService _service;
        private ClientManageVM _clients;

        public ICommand EditClientCommand { get; set; }

        public ClientsVM SelectedClient
        {
            get { return _service.SelectedClient; }
        }

        public ClientInfoVM(IDataService service, ClientManageVM clients)
        {
            _service = service;
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
            if (IsLetter(SelectedClient.ClientName) && IsLetterNumber(SelectedClient.PostalCode) && IsLetter(SelectedClient.Street) && IsNumber(SelectedClient.Housenumber) && IsLetter(SelectedClient.Country) && IsPhoneNumber(SelectedClient.Phone))
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
            if (input == null)
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
