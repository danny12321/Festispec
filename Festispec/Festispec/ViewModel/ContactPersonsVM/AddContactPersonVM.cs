using Festispec.Domain;
using Festispec.ViewModel.DataService;
using Festispec.ViewModel.FestivalVM;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Festispec.ViewModel.ContactPersonsVM
{
    public class AddContactPersonVM
    {
        private FestivalInfoVM _festival;
        private IDataService _service;

        public ContactPersonVM ContactPerson { get; set; }

        public ICommand SaveCommand { get; set; }

        public AddContactPersonVM(FestivalInfoVM festival, IDataService service)
        {
            this._service = service;
            _festival = festival;
            ContactPerson = new ContactPersonVM();

            ContactPerson.ClientId = _service.SelectedClient.ClientId;
            ContactPerson.FestivalId = _service.SelectedFestival.FestivalId;

            SaveCommand = new RelayCommand(SaveContact, CanSaveContact);
        }

        private bool CanSaveContact()
        {
            return true;
        }

        private void SaveContact()
        {
            _festival.Contactpersons.Add(ContactPerson);

            using (var context = new FestispecEntities())
            {
                context.Contactpersons.Add(ContactPerson.ToModel());
                context.SaveChanges();
            }
            _festival.ShowFestivalPage();
        }
    }
}
