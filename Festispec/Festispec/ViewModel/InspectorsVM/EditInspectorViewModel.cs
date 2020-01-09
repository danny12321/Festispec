using Festispec.Domain;
using Festispec.Utils;
using Festispec.ViewModel.DataService;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Festispec.ViewModel.InspectorsVM
{
    public class EditInspectorViewModel : ViewModelBase
    {
        private InspectorListViewModel _inspectorViewModel;
        private MainViewModel _main;
        private IDataService _service;
        public bool Approved { get; set; }
        public ICommand EditInspectorCommand { get; set; }
        
        public ICommand GenerateLatLongBasedOnAdressCommand { get; set; }

        private string _selectedCountry;
        public ObservableCollection<string> ComboList { get; set; }

        public string SelectedCountry
        {
            get { return _selectedCountry; }
            set { _selectedCountry = value; }
        }

        public InspectorviewModel SelectedInspector
        {
            get
            {
                return _service.SelectedInspector;
            }
        }
        public EditInspectorViewModel(InspectorListViewModel i, DataService.IDataService dataService, MainViewModel main)
        {
            _main = main;
            _service = dataService;
            _inspectorViewModel = i;
            EditInspectorCommand = new RelayCommand(EditInspectorMethod);
            
            GenerateLatLongBasedOnAdressCommand = new RelayCommand(GenerateLatLongBasedOnAdress);

            using (var context = new FestispecEntities())
            {
                _selectedCountry = SelectedInspector.Country;
            }
            ComboList = new ObservableCollection<string>();
            ComboList.Add("Nederland");
            ComboList.Add("België");
            Approved = _service.SelectedInspector.Active != null;
        }

        private void EditInspectorMethod()
        {
            if (CanEditInspector())
            {
                if (Approved)
                {
                    if (_service.SelectedInspector.Active == null)
                    {
                        _service.SelectedInspector.Active = DateTime.Now;

                    }
                }
                else
                {
                    _service.SelectedInspector.Active = null;
                }

                using (var context = new FestispecEntities())
                {
                    context.Entry(_inspectorViewModel.SelectedInspector.ToModel()).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                }
                _inspectorViewModel.ShowInspectorPage();
            }
        }

        public bool CanEditInspector()
        {
            if (!IsEmptyField(_inspectorViewModel.SelectedInspector.InspectorFirstName) && !IsEmptyField(_inspectorViewModel.SelectedInspector.InspectorLastName))
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
            if (SelectedInspector.Country != null || SelectedInspector.City != null)
            {
                LatLongGenerator latLongGenerator = new LatLongGenerator();

                Task<string> latLongGeneratorAwait = latLongGenerator.GenerateLatLong(SelectedInspector.Country, SelectedInspector.City, SelectedInspector.Street, SelectedInspector.Housenumber);
                string latlong = await latLongGeneratorAwait;

                SelectedInspector.Latitude = latlong.Split(',')[0];
                SelectedInspector.Longitude = latlong.Split(',')[1];
                RaisePropertyChanged("InspectorViewModel");
            }
        }
    }
}
