using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Festispec.Domain;
using GalaSoft.MvvmLight.CommandWpf;
using Microsoft.Maps.MapControl.WPF;

namespace Festispec.ViewModel.Inspections
{
    public class InspectorsVM
    {

        public Inspectors Inspector { get; set; }
        public string Fullname { get { return Inspector.name + " " + Inspector.lastname; } }

        public string Pos { get { return Inspector.latitude + "," + Inspector.longitude; } }

        public TimeSpan TravelTime { get; set; }

        public ICommand SetViewToSelectedPersonCommand { get; set; }

        public bool HasPos { get
            {
                if (Inspector.latitude != null && Inspector.longitude != null)
                {
                    return true;
                }
                return false;
            } 
        }

        public InspectorsVM(Inspectors inspectors)
        {
            SetViewToSelectedPersonCommand = new RelayCommand<Map>(SetViewToSelectedPerson);
            Inspector = inspectors;
        }

        private void SetViewToSelectedPerson(Map map)
        {
            if (Inspector.longitude != null || Inspector.latitude != null)
            {
                var longitude = Convert.ToDouble(Inspector.longitude.Replace('.', ','));
                var latitude = Convert.ToDouble(Inspector.latitude.Replace('.', ','));
                var location = new Location(latitude, longitude);
                map.SetView(location, 10);
            }
        }
    }
}
