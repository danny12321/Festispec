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
    public class ClientInfoVM
    {
        private IDataService _service;

        public ICommand EditClientCommand { get; set; }

        public ClientsVM SelectedClient
        {
            get { return _service.SelectedClient; }
        }

        public ClientInfoVM(IDataService service)
        {
            _service = service;
            EditClientCommand = new RelayCommand(SaveClient, CanSaveClient);
        }

        private bool CanSaveClient()
        {
            return true;
        }

        private void SaveClient()
        {
            using (var context = new FestispecEntities())
            {
                context.Entry(SelectedClient.ToModel()).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
