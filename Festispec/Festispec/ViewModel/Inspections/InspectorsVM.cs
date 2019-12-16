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

        private Inspectors _inspector;
        
        public string Fullname { get { return _inspector.name + " " + _inspector.lastname; } }

        public string Pos { get { return _inspector.latitude + "," + _inspector.longitude; } }

        public TimeSpan TravelTime { get; set; }

        public ICommand SetViewToSelectedPersonCommand { get; set; }

        public bool HasPos { get
            {
                if (_inspector.latitude != null && _inspector.longitude != null)
                {
                    return true;
                }
                return false;
            } 
        }
        public InspectorsVM()
        {
            SetViewToSelectedPersonCommand = new RelayCommand<Map>(SetViewToSelectedPerson);
            _inspector = new Inspectors();
        }
        public InspectorsVM(Inspectors inspectors)
        {
            SetViewToSelectedPersonCommand = new RelayCommand<Map>(SetViewToSelectedPerson);
            _inspector = inspectors;
        }

        private void SetViewToSelectedPerson(Map map)
        {
            if (_inspector.longitude != null || _inspector.latitude != null)
            {
                var longitude = Convert.ToDouble(_inspector.longitude.Replace('.', ','));
                var latitude = Convert.ToDouble(_inspector.latitude.Replace('.', ','));
                var location = new Location(latitude, longitude);
                map.SetView(location, 10);
            }
        }

        public Inspectors ToModel()
        {
            return _inspector;
        }

        public int id { get { return _inspector.id; } set { _inspector.id = value; } }
        public string name { get { return _inspector.name; } set { _inspector.name = value; } }
        public string lastname { get { return _inspector.lastname; } set { _inspector.lastname = value; } }
        public Nullable<System.DateTime> birthday { get { return _inspector.birthday; } set { _inspector.birthday = value; } }
        public string email { get { return _inspector.email; } set { _inspector.email = value; } }
        public string postalcode { get { return _inspector.postalcode; } set { _inspector.postalcode = value; } }
        public string country { get { return _inspector.country; } set { _inspector.country = value; } }
        public string city { get { return _inspector.city; } set { _inspector.city = value; } }
        public string street { get { return _inspector.street; } set { _inspector.street = value; } }
        public string housenumber { get { return _inspector.housenumber; } set { _inspector.housenumber = value; } }
        public string phone { get { return _inspector.phone; } set { _inspector.phone = value; } }
        public Nullable<System.DateTime> active { get { return _inspector.active; } set { _inspector.active = value; } }
        public string latitude { get { return _inspector.latitude; } set { _inspector.latitude = value; } }
        public string longitude { get { return _inspector.longitude; } set { _inspector.longitude = value; } }
    }
}
