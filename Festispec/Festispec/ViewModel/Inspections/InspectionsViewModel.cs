using Festispec.Domain;
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
        public ObservableCollection<InspectionVM> Inspections { get; set; }
        public ICommand NavigateAddInspectionCommand { get; set; }

        public InspectionsViewModel(MainViewModel main)
        {

            _main = main;

            NavigateAddInspectionCommand = new RelayCommand(NavigateAddInspection);

            using (var context = new FestispecEntities())
            {
                var inspections = context.Inspections.ToList().Select(i => new InspectionVM(i));
                Inspections = new ObservableCollection<InspectionVM>(inspections);
                Console.WriteLine();
            }
        }

        private void NavigateAddInspection()
        {
            _main.SetPage("AddInspection", false);
        }
    }
}
