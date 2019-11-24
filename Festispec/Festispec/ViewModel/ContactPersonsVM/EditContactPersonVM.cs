using Festispec.Domain;
using Festispec.ViewModel.DataService;
using Festispec.ViewModel.FestivalVM;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Festispec.ViewModel.ContactPersonsVM
{
    public class EditContactPersonVM
    {
        private IDataService _service;
        private FestivalInfoVM _festival;

        public ICommand SaveCommand { get; set; }

        public ContactPersonVM SelectedContactPerson
        {
            get { return _service.SelectedContactPerson; }
        }

        public EditContactPersonVM(IDataService service, FestivalInfoVM festival)
        {
            _service = service;
            _festival = festival;

            SaveCommand = new RelayCommand(SaveChanges, canSaveChanges);
        }

        private bool canSaveChanges()
        {
            return true;
        }

        private void SaveChanges()
        {
            using (var context = new FestispecEntities())
            {
                context.Entry(SelectedContactPerson.ToModel()).State = EntityState.Modified;
                context.SaveChanges();
            }
            _festival.ShowFestivalPage();
        }
    }
}
