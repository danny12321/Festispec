using Festispec.Domain;
using Festispec.ViewModel.ClientVM;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Festispec.ViewModel.Festival_VMs
{
    public class AddFestivalVM
    {
        private ClientInfoVM _clients;

        public ICommand AddFestivalCommand { get; set; }

        public FestivalVM.FestivalVM Festival { get; set; }

        public AddFestivalVM(ClientInfoVM client)
        {
            this._clients = client;
            Festival = new FestivalVM.FestivalVM();

            AddFestivalCommand = new RelayCommand(AddFestivalMethod, CanAddFestival);

            Festival.ClientId = _clients.SelectedClient.ClientId;
        }

        private bool CanAddFestival()
        {
            return true;
        }

        private void AddFestivalMethod()
        {
            _clients.Festivals.Add(Festival);

            using (var context = new FestispecEntities())
            {
                context.Festivals.Add(Festival.ToModel());
                context.SaveChanges();
            }
            _clients.ShowClientPage();
        }
    }
}
