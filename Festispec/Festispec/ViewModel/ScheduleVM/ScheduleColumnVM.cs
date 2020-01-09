using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Festispec.Domain;
using Festispec.ViewModel.DataService;
using Festispec.ViewModel.Inspections;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace Festispec.ViewModel.ScheduleVM
{
    public class ScheduleColumnVM : ViewModelBase
    {
        public ObservableCollection<InspectionVM> Inspections { get; set; }

        public ICommand OpenCommand { get; set; }

        private DateTime _date;

        public string Date
        {
            get
            {
                return _date.Date.ToLongDateString();
            }
        }

        private IDataService _service;

        private MainViewModel _main;

        public ScheduleColumnVM(DateTime date, MainViewModel main, IDataService service)
        {
            _date = date;
            _main = main;
            _service = service;
            OpenCommand = new RelayCommand<InspectionVM>(Open);
            RaisePropertyChanged("Date");

            using (var context = new FestispecEntities())
            {
                var inspections = context.Inspections.ToList().Where(i => i.start_date.Date == date.Date).Select(i => new InspectionVM(i));
                Inspections = new ObservableCollection<InspectionVM>(inspections);
            }
        }

        private void Open(InspectionVM inspection)
        {
            using (var context = new FestispecEntities())
            {
                var festival = context.Festivals.First(f => f.id == inspection.Festival_id);
                _service.SelectedFestival = new FestivalVM.FestivalVM(festival);
                
                _service.SelectedInspection = inspection;
                _main.SetPage("EditInspection");
            }
        }
    }
}
