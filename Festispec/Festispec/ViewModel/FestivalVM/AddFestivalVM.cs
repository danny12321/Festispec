using Festispec.Domain;
using Festispec.Utils;
using Festispec.ViewModel.ClientVM;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Festispec.ViewModel.Festival_VMs
{
    public class AddFestivalVM : ViewModelBase
    {
        private ClientInfoVM _clients;

        public TimeSpan StartTime
        {
            get { return Festival.StartTime; }
            set { Festival.StartTime = value; }
        }

        public DateTime StartDateBegin
        {
            get { return Festival.StartDateBegin; }
            set { Festival.StartDateBegin = value; }
        }

        public TimeSpan EndTime
        {
            get { return Festival.EndTime; }
            set { Festival.EndTime = value; }
        }

        public DateTime EndDateEnd
        {
            get { return Festival.EndDateEnd; }
            set { Festival.EndDateEnd = value; }
        }

        public ICommand AddFestivalCommand { get; set; }
        public ICommand GenerateLatLongBasedOnAdressCommand { get; set; }

        public FestivalVM.FestivalVM Festival { get; set; }

        public AddFestivalVM(ClientInfoVM client)
        {
            this._clients = client;
            Festival = new FestivalVM.FestivalVM();

            AddFestivalCommand = new RelayCommand(AddFestivalMethod, CanAddFestival);
            GenerateLatLongBasedOnAdressCommand = new RelayCommand(GenerateLatLongBasedOnAdress);

            Festival.ClientId = _clients.SelectedClient.ClientId;

            StartDateBegin = DateTime.Now;
            StartTime = DateTime.Now.TimeOfDay;
            EndDateEnd = DateTime.Now;
            EndTime = DateTime.Now.TimeOfDay;
        }

        private bool CanAddFestival()
        {
            if (IsMatch())
            {
                return true;
            }
            return false;
        }

        private void AddFestivalMethod()
        {
            Festival.StartDate = (StartDateBegin + StartTime);
            Festival.EndDate = (EndDateEnd + EndTime);
            _clients.Festivals.Add(Festival);

            using (var context = new FestispecEntities())
            {
                context.Festivals.Add(Festival.ToModel());
                context.SaveChanges();
            }
            _clients.ShowClientPage();
        }

        private bool IsMatch()
        {
            if (!IsEmptyField(Festival.FestivalName) && !IsEmptyField(Festival.PostalCode) && !IsEmptyField(Festival.Street) && !IsEmptyField(Festival.HouseNumber) && !IsEmptyField(Festival.Country) && !IsEmptyField(Festival.Longitude) && !IsEmptyField(Festival.Latitude))
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
            if (Festival.Country != null || Festival.City != null)
            {
                LatLongGenerator latLongGenerator = new LatLongGenerator();

                Task<string> latLongGeneratorAwait = latLongGenerator.GenerateLatLong(Festival.Country, Festival.City, Festival.Street, Festival.HouseNumber);
                string latlong = await latLongGeneratorAwait;

                Festival.Latitude = latlong.Split(',')[0];
                Festival.Longitude = latlong.Split(',')[1];
                RaisePropertyChanged("Festival");
            }
        }
    }
}
