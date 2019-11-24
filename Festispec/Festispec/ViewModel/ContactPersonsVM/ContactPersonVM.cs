using Festispec.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel.ContactPersonsVM
{
    public class ContactPersonVM
    {
        private Contactpersons _contactperson;

        public ContactPersonVM(Contactpersons contactperson)
        {
            this._contactperson = contactperson;
        }

        public ContactPersonVM()
        {
            _contactperson = new Contactpersons();
        }

        public int ContactPersonId
        {
            get { return _contactperson.id; }
            set { _contactperson.id = value; }
        }

        public string ContactPersonName
        {
            get { return _contactperson.name; }
            set { _contactperson.name = value; }
        }

        public string ContactPersonLastName
        {
            get { return _contactperson.lastname; }
            set { _contactperson.lastname = value; }
        }

        public string ContactPersonMail
        {
            get { return _contactperson.email; }
            set { _contactperson.email = value; }
        }

        public string ContactPersonPhoneNumber
        {
            get { return _contactperson.phone; }
            set { _contactperson.phone = value; }
        }

        public int? FestivalId
        {
            get { return _contactperson.festival_id; }
            set { _contactperson.festival_id = value; }
        }

        public int? ClientId
        {
            get { return _contactperson.client_id; }
            set { _contactperson.client_id = value; }
        }

        public int TypeContact
        {
            get { return _contactperson.type_contact; }
            set { _contactperson.type_contact = value; }
        }

        internal Contactpersons ToModel()
        {
            return _contactperson;
        }
    }
}
