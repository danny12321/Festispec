using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Festispec.Domain;

namespace Festispec.ViewModel.InspectorsVM
{
    public class InspectorviewModel
    {
        private Inspectors _i;
        public InspectorviewModel()
        {
            _i = new Inspectors();
        }
        public InspectorviewModel(Inspectors i)
        {
            _i = i;
        }
        public int InspectorId
        {
            get { return _i.id; }
            set { _i.id = value; }
        }

        public string InspectorFirstName
        {
            get { return _i.name; }
            set { _i.name = value; }
        }
        public string InspectorLastName
        {
            get { return _i.lastname; }
            set { _i.lastname = value; }
        }
        public DateTime Birthday
        {
            get
            {
                return _i.birthday ?? DateTime.Now;
            }
            set
            {
                _i.birthday = value;
            }
        }

        public string PostalCode
        {
            get { return _i.postalcode; }
            set { _i.postalcode = value; }
        }

        public string Street
        {
            get { return _i.street; }
            set { _i.street = value; }
        }

        public string Housenumber
        {
            get { return _i.housenumber; }
            set { _i.housenumber = value; }
        }


        public DateTime? Active
        {
            get
            {
                return _i.active;
            }
            set
            {
                _i.active = value;
            }
        }

        public string Phone
        {
            get { return _i.phone; }
            set { _i.phone = value; }
        }

        internal Inspectors ToModel()
        {
            return _i;
        }
    }
}
