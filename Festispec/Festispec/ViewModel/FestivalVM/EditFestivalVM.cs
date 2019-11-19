using Festispec.Domain;
using Festispec.ViewModel.ClientVM;
using Festispec.ViewModel.DataService;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
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

        private ClientInfoVM _clients;

        public ICommand EditFestivalCommand { get; set; }

        public FestivalVM SelectedFestival
        {
            get { return _service.SelectedFestival; }
        }

        public TimeSpan StartTime
        {
            get { return SelectedFestival.StartTime; }
            set { SelectedFestival.StartTime = value; }
        }

        public DateTime StartDateBegin
        {
            get { return SelectedFestival.StartDateBegin; }
            set { SelectedFestival.StartDateBegin = value; }
        }

        public TimeSpan EndTime
        {
            get { return SelectedFestival.EndTime; }
            set { SelectedFestival.EndTime = value; }
        }

        public DateTime EndDateEnd
        {
            get { return SelectedFestival.EndDateEnd; }
            set { SelectedFestival.EndDateEnd = value; }
        }

        public EditFestivalVM(MainViewModel main, IDataService service, ClientInfoVM clients)
        {
            this._main = main;
            this._service = service;
            this._clients = clients;

            EditFestivalCommand = new RelayCommand(SaveClient, CanSaveClient);

            StartDateBegin = SelectedFestival.StartDate.Date;
            StartTime = SelectedFestival.StartDate.TimeOfDay;
            EndDateEnd = SelectedFestival.EndDate.Date;
            EndTime = SelectedFestival.EndDate.TimeOfDay;
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
            SelectedFestival.StartDate = (StartDateBegin + StartTime);
            SelectedFestival.EndDate = (EndDateEnd + EndTime);

            using (var context = new FestispecEntities())
            {
                context.Entry(SelectedFestival.ToModel()).State = EntityState.Modified;
                context.SaveChanges();
            }
            _clients.ShowClientPage();
        }

        private bool IsMatch()
        {
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
