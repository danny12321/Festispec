using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Festispec.Domain;
using Festispec.ViewModel.ContactPersonsVM;
using GalaSoft.MvvmLight;

namespace Festispec.ViewModel.FestivalVM
{
    public class FestivalVM : ViewModelBase
    {
        private Festivals _festival;
        private TimeSpan _StartTime;
        private DateTime _StartDateBegin;
        private TimeSpan _EndTime;
        private DateTime _EndDateEnd;

        public FestivalVM(Festivals festivals)
        {
            this._festival = festivals;
        }

        public FestivalVM()
        {
            _festival = new Festivals();
        }

        public int FestivalId
        {
            get { return _festival.id; }
            set { _festival.id = value; }
        }

        public string FestivalName
        {
            get { return _festival.name; }
            set { _festival.name = value; }
        }

        public string PostalCode
        {
            get { return _festival.postalcode; }
            set { _festival.postalcode = value; }
        }

        public string City
        {
            get { return _festival.city; }
            set { _festival.city = value; }
        }

        public string Street
        {
            get { return _festival.street; }
            set { _festival.street = value; }
        }

        public string HouseNumber
        {
            get { return _festival.housenumber; }
            set { _festival.housenumber = value; }
        }

        public string Country
        {
            get { return _festival.country; }
            set { _festival.country = value; }
        }

        public DateTime StartDate
        {
            get { return _festival.start_date; }
            set { _festival.start_date = value; }
        }

        public DateTime EndDate
        {
            get { return _festival.end_date; }
            set { _festival.end_date = value; }
        }

        public int? Year
        {
            get { return _festival.start_date.Year; }
        }

        public int ClientId
        {
            get { return _festival.client_id; }
            set { _festival.client_id = value; }
        }

        public int? MunicipalityId
        {
            get { return _festival.municipality_id; }
            set { _festival.municipality_id = value; }
        }

        public string Latitude
        {
            get { return _festival.latitude; }
            set { _festival.latitude = value; }
        }

        public string Longitude
        {
            get { return _festival.longitude; }
            set { _festival.longitude = value; }
        }

        public TimeSpan StartTime
        {
            get { return _StartTime; }
            set { _StartTime = value; }
        }

        public DateTime StartDateBegin
        {
            get { return _StartDateBegin; }
            set { _StartDateBegin = value; }
        }

        public TimeSpan EndTime
        {
            get { return _EndTime; }
            set { _EndTime = value; }
        }

        public DateTime EndDateEnd
        {
            get { return _EndDateEnd; }
            set { _EndDateEnd = value; }
        }

        public List<ContactPersonVM> ContactPersons
        {
            get
            {
                return _festival.Contactpersons.Select(c => new ContactPersonVM(c)).ToList();
            }
        }

        internal Festivals ToModel()
        {
            return _festival;
        }
    }
}
