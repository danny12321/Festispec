using Festispec.Domain;
using Festispec.View.FestivalViews;
using Festispec.ViewModel.DataService;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Newtonsoft.Json;
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

            using (var context = new FestispecEntities())
            {
                var festival = context.Festivals.ToList()
                             .Select(e => new FestivalVM.FestivalVM(e));

                FestivalList = new ObservableCollection<FestivalVM.FestivalVM>(festival);
            }
            ShowFestival = new RelayCommand(ShowFestivalInfo);
            ShowAddInspection = new RelayCommand(NavigateInspection);

            CreateOfflineFestivalData();
        }

        private void ShowFestivalInfo()
        {
            _main.SetPage("FestivalInfo", false);
        }

        private void NavigateInspection()
        {
            _main.SetPage("Inspections", false);
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
