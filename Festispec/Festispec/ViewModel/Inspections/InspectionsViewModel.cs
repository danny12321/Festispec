using Festispec.Domain;
using Festispec.ViewModel.DataService;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Festispec.ViewModel.Inspections
{
    public class InspectionsViewModel
    {
        private MainViewModel _main;
        private int _festivalId;
        public ObservableCollection<InspectionVM> Inspections { get; set; }
        public ICommand NavigateAddInspectionCommand { get; set; }
        public ICommand NavigateEditInspectionCommand { get; set; }
        private IDataService _service;

        public InspectionsViewModel(MainViewModel main, IDataService service)
        {
            _main = main;
            _service = service;
            _festivalId = service.SelectedFestival.FestivalId;

            NavigateAddInspectionCommand = new RelayCommand(NavigateAddInspection);
            NavigateEditInspectionCommand = new RelayCommand<InspectionVM>(NavigateEditInspection);

            using (var context = new FestispecEntities())
            {
                var inspections = context.Inspections.ToList().Where(i => i.festival_id == _festivalId).Select(i => new InspectionVM(i));
                Inspections = new ObservableCollection<InspectionVM>(inspections);
                Console.WriteLine();
            }
        }

        private void NavigateAddInspection()
        {
            _main.SetPage("AddInspection");
        }

        private void NavigateEditInspection(InspectionVM inspection)
        {
            _service.SelectedInspection = inspection;
            _main.SetPage("EditInspection");
        }
    }
}
