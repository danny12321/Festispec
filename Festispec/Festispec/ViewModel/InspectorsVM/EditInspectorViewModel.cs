using Festispec.Domain;
using Festispec.Utils;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Festispec.ViewModel.InspectorsVM
{
    public class EditInspectorViewModel : ViewModelBase
    {
        private InspectorListViewModel _inspectorViewModel;
        public ICommand EditInspectorCommand { get; set; }
        public ICommand GenerateLatLongBasedOnAdressCommand { get; set; }
        public InspectorListViewModel InspectorViewModel
        {
            get
            {
                return _inspectorViewModel;
            }
        }
        public EditInspectorViewModel(InspectorListViewModel i)
        {
            _inspectorViewModel = i;
            EditInspectorCommand = new RelayCommand(EditInspectorMethod);
            GenerateLatLongBasedOnAdressCommand = new RelayCommand(GenerateLatLongBasedOnAdress);
        }
        private void EditInspectorMethod()
        {
            if (CanEditInspector())
            {
                

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
            if (!IsEmptyField(_inspectorViewModel.SelectedInspector.Housenumber) && !IsEmptyField(_inspectorViewModel.SelectedInspector.InspectorFirstName) && !IsEmptyField(_inspectorViewModel.SelectedInspector.InspectorLastName) && !IsEmptyField(_inspectorViewModel.SelectedInspector.InspectorFirstName)
                 && !IsEmptyField(_inspectorViewModel.SelectedInspector.Phone) && !IsEmptyField(_inspectorViewModel.SelectedInspector.PostalCode) && !IsEmptyField(_inspectorViewModel.SelectedInspector.Street))
            {
                return true;
            }
            return false;
        }

        //private bool IsMatch()
        //{
        //    if (IsLetter(Inspector.InspectorName) && IsLetterNumber(Inspector.PostalCode) && IsLetter(Inspector.Street) && IsNumber(Inspector.Housenumber) && IsLetter(Inspector.Country) && IsPhoneNumber(Inspector.Phone))
        //    {
        //        return true;
        //    }
        //    return false;
        //}

        //private bool IsLetter(string input)
        //{
        //    if (!IsEmptyField(input))
        //    {
        //        if (Regex.IsMatch(input, @"^^(?! )[A-Za-z\s]+$"))
        //        {
        //            return true;
        //        }
        //    }
        //    return false;
        //}

        //private bool IsNumber(string input)
        //{
        //    if (!IsEmptyField(input))
        //    {
        //        if (Regex.IsMatch(input, @"^^(?! )[0-9\s]+$"))
        //        {
        //            return true;
        //        }
        //    }
        //    return false;
        //}

        //private bool IsLetterNumber(string input)
        //{
        //    if (!IsEmptyField(input))
        //    {
        //        if (Regex.IsMatch(input, @"^^(?! )[A-Za-z0-9\s]+$"))
        //        {
        //            return true;
        //        }
        //    }
        //    return false;
        //}

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
            if (InspectorViewModel.SelectedInspector.Country != null || InspectorViewModel.SelectedInspector.City != null)
            {
                LatLongGenerator latLongGenerator = new LatLongGenerator();

                Task<string> latLongGeneratorAwait = latLongGenerator.GenerateLatLong(InspectorViewModel.SelectedInspector.Country, InspectorViewModel.SelectedInspector.City, InspectorViewModel.SelectedInspector.Street, InspectorViewModel.SelectedInspector.Housenumber);
                string latlong = await latLongGeneratorAwait;

                InspectorViewModel.SelectedInspector.Latitude = latlong.Split(',')[0];
                InspectorViewModel.SelectedInspector.Longitude = latlong.Split(',')[1];
                RaisePropertyChanged("InspectorViewModel");
            }
        }
    }
}
