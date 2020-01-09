using Festispec.Domain;
using Festispec.Utils;
using Festispec.ViewModel.ClientVM;
using Festispec.ViewModel.DataService;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private string _selectedCountry;
        public ObservableCollection<string> ComboList { get; set; }

        public ICommand EditFestivalCommand { get; set; }
        public ICommand GenerateLatLongBasedOnAdressCommand { get; set; }

        public string SelectedCountry
        {
            get { return _selectedCountry; }
            set { _selectedCountry = value; }
        }

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
            GenerateLatLongBasedOnAdressCommand = new RelayCommand(GenerateLatLongBasedOnAdress);

            using (var context = new FestispecEntities())
            {
                _selectedCountry = SelectedFestival.Country;
            }

            ComboList = new ObservableCollection<string>();
            ComboList.Add("Nederland");
            ComboList.Add("België");

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
            if (!IsEmptyField(SelectedFestival.FestivalName))
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

        private async void GenerateLatLongBasedOnAdress()
        {
            //You need atleast a country and a city to get a good result
            if (SelectedFestival.Country != null || SelectedFestival.City != null)
            {
                LatLongGenerator latLongGenerator = new LatLongGenerator();

                Task<string> latLongGeneratorAwait = latLongGenerator.GenerateLatLong(SelectedFestival.Country, SelectedFestival.City, SelectedFestival.Street, SelectedFestival.HouseNumber);
                string latlong = await latLongGeneratorAwait;

                SelectedFestival.Latitude = latlong.Split(',')[0];
                SelectedFestival.Longitude = latlong.Split(',')[1];
                RaisePropertyChanged("SelectedFestival");
            }
        }
    }
}
