using Festispec.Domain;
using Festispec.View.FestivalViews;
using Festispec.ViewModel.DataService;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Festispec.ViewModel.FestivalVMs
{
    public class FestivalManagementVM : ViewModelBase
    {
        private IDataService _service;
        private MainViewModel _main;

        public ObservableCollection<FestivalVM.FestivalVM> FestivalList { get; set; }

        public ICommand ShowFestival { get; set; }
        public ICommand ShowAddInspection { get; set; }

        public FestivalVM.FestivalVM SelectedFestival
        {
            get { return _service.SelectedFestival; }
            set
            {
                _service.SelectedFestival = value;
                RaisePropertyChanged();
            }
        }

        public FestivalManagementVM(MainViewModel main, IDataService service)
        {
            this._main = main;
            this._service = service;

            if (_service.IsOffline)
            {
                GetDataFromJson();
            } else
            {
                using (var context = new FestispecEntities())
                {
                    var festival = context.Festivals.ToList()
                                 .Select(e => new FestivalVM.FestivalVM(e));

                    FestivalList = new ObservableCollection<FestivalVM.FestivalVM>(festival);
                }

                CreateOfflineFestivalData();
            }

            ShowFestival = new RelayCommand(ShowFestivalInfo);
            ShowAddInspection = new RelayCommand(NavigateInspection);
        }

        private void GetDataFromJson()
        {
            // Get old JSON
            string fileName = "Festivals.json";
            string path = Path.Combine(Environment.CurrentDirectory, @"Offline\", fileName);
            string jsonfestivalsData = File.ReadAllText(path);

            // Parse JSON to object
            JArray parsedFestivalsJson = JArray.Parse(jsonfestivalsData);

            FestivalList = new ObservableCollection<FestivalVM.FestivalVM>();

            for (int i = 0; i < parsedFestivalsJson.Count; i++)
            {
                var festivalVM = new FestivalVM.FestivalVM();
                festivalVM.FestivalId = parsedFestivalsJson[i]["FestivalId"].ToObject<int>();
                festivalVM.FestivalName = parsedFestivalsJson[i]["FestivalName"].ToString();
                festivalVM.PostalCode = parsedFestivalsJson[i]["PostalCode"].ToString();
                festivalVM.City = parsedFestivalsJson[i]["City"].ToString();
                festivalVM.Street = parsedFestivalsJson[i]["Street"].ToString();
                festivalVM.HouseNumber = parsedFestivalsJson[i]["HouseNumber"].ToString();
                festivalVM.Country = parsedFestivalsJson[i]["Country"].ToString();
                festivalVM.StartDate = parsedFestivalsJson[i]["StartDate"].ToObject<DateTime>();
                festivalVM.EndDate = parsedFestivalsJson[i]["EndDate"].ToObject<DateTime>();
                festivalVM.ClientId = parsedFestivalsJson[i]["ClientId"].ToObject<int>();
                festivalVM.MunicipalityId = parsedFestivalsJson[i]["MunicipalityId"].ToObject<int?>();
                festivalVM.Latitude = parsedFestivalsJson[i]["Latitude"].ToString();
                festivalVM.Longitude = parsedFestivalsJson[i]["Longitude"].ToString();

                FestivalList.Add(festivalVM);
            }
        }

        private void ShowFestivalInfo()
        {
            _main.SetPage("FestivalInfo");
        }

        private void NavigateInspection()
        {
            _main.SetPage("Inspections");
        }

        private void CreateOfflineFestivalData()
        {
            // TODO When login is up to date make user dynamic
            string fileContent = JsonConvert.SerializeObject(FestivalList.ToList());

            string fileName = "Festivals.json";
            string path = Path.Combine(Environment.CurrentDirectory, @"Offline\", fileName);

            Directory.CreateDirectory("Offline");

            using (StreamWriter outputFile = new StreamWriter(path))
            {
                outputFile.WriteLine(fileContent);
            }
        }
    }
}
