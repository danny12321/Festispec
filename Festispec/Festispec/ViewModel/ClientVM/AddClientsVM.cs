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
            try
            {
                _clients.Clients.Add(Client);

                using (var context = new FestispecEntities())
                {
                    context.Clients.Add(Client.ToModel());
                    context.SaveChanges();
                }
                _clients.ShowClientPage();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var entityValidationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationErrors.ValidationErrors)
                    {
                        Console.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                    }
                }
            }

        }

        public bool CanAddClient()
        {
            return true;
        }

    }
}
