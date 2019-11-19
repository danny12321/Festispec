using Festispec.Domain;
using Festispec.ViewModel.ClientVM;
using Festispec.ViewModel.DataService;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;


namespace Festispec.ViewModel.FestivalVM
{
    public class EditFestivalVM : ViewModelBase
    {
        private IDataService _service;
        private MainViewModel _main;

        private ClientManageVM _clients;

        public ICommand EditFestivalCommand { get; set; }

        public FestivalVM SelectedFestival
        {
            get { return _service.SelectedFestival; }
        }

        public EditFestivalVM(MainViewModel main, IDataService service, ClientManageVM clients)
        {
            this._main = main;
            this._service = service;
            this._clients = clients;

            EditFestivalCommand = new RelayCommand(SaveClient, CanSaveClient);
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
                context.Entry(SelectedFestival.ToModel()).State = EntityState.Modified;
                context.SaveChanges();
            }
            _clients.ShowClientPage();
        }

        private bool IsMatch()
        {
            //&& !IsEmptyField(SelectedFestival.Latitude) && !IsEmptyField(SelectedFestival.Longitude)
            if (!IsEmptyField(SelectedFestival.FestivalName) && !IsEmptyField(SelectedFestival.PostalCode) && !IsEmptyField(SelectedFestival.Street) && !IsEmptyField(SelectedFestival.HouseNumber) && !IsEmptyField(SelectedFestival.Country))
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
