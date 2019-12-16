using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Festispec.Domain;
using Festispec.ViewModel.DataService;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Microsoft.Maps.MapControl.WPF;

namespace Festispec.ViewModel.Inspections
{
    public class InspectionAddViewModel : ViewModelBase
    {
        private int _festivalId;
    
        public ObservableCollection<InspectorsVM> Inspectors { get; set; }

        public ObservableCollection<InspectorAtInspectionVM> InspectorsAtInspection { get; set; }

        public ObservableCollection<InspectorsVM> SelectedInspectors { get; set; }


        public FestivalVM Festival { get; set; }

        public InspectionVM Inspection { get; set; }

        private DateTime _startDate;
        public DateTime StartDate { get { return _startDate; } set { _startDate = value; EndDate = value; RaisePropertyChanged("EndDate"); } }

        private TimeSpan _startTime;
        public TimeSpan StartTime { get { return _startTime; } set { _startTime = value; EndTime = value; RaisePropertyChanged("EndTime"); } }

        public DateTime EndDate { get; set; }
        public TimeSpan EndTime { get; set; }

        public DateTime EndDateTimeCombined
        {
            get { return EndDate + EndTime; }
        }

        public DateTime StartDateTimeCombined {
            get { return StartDate + StartTime; }
        }

        private MainViewModel _main;

        public ICommand TestButton { get; set; }
        public ICommand AddInspectionCommand { get; set; }
        public ICommand SetViewToSelectedPersonCommand { get; set; }
        public ICommand SelectInspectorCommand { get; set; }
        public ICommand DelectInspectorCommand { get; set; }

        public IDataService _service;

        public InspectionAddViewModel(MainViewModel main, IDataService service)
        {
            _main = main;
            _service = service;

            _festivalId = service.SelectedFestival.FestivalId;

            SelectedInspectors = new ObservableCollection<InspectorsVM>();
            Inspection = new InspectionVM();

            StartDate = DateTime.Now;
            StartTime = DateTime.Now.TimeOfDay;

            EndDate = DateTime.Now;
            EndTime = DateTime.Now.TimeOfDay;

            TestButton = new RelayCommand(Debug);
            AddInspectionCommand = new RelayCommand(AddInspection);
            SelectInspectorCommand = new RelayCommand<InspectorsVM>(SelectInspector);
            DelectInspectorCommand = new RelayCommand<InspectorsVM>(DelectInspector);

            using (var context = new FestispecEntities())
            {
                //Get inspectors
                var inspectors = context.Inspectors.ToList()
                    .Select(i => new InspectorsVM(i));

                Inspectors = new ObservableCollection<InspectorsVM>(inspectors);
                   
                Festival = new FestivalVM(context.Festivals.ToList().First(f => f.id == _festivalId));
            }
        }

        private void AddInspection()
        {
            // Combining all values in here instead of binding directly to the view because the start and end datetime is separate
            Inspection.Start_date = StartDateTimeCombined;
            Inspection.End_date = EndDateTimeCombined;

            // TODO Make festival id dynamic
            Inspection.Festival_id = _festivalId;

            InspectorsAtInspection = new ObservableCollection<InspectorAtInspectionVM>();

            if (ValidateInput(Inspection))
            {
                using (var context = new FestispecEntities())
                {
                    context.Inspections.Add(Inspection.ToModel());
                    context.SaveChanges();
                }

                SelectedInspectors.ToList().ForEach(i => {
                    var inspector_at_inspection = new Inspectors_at_inspection();
                    inspector_at_inspection.inpector_id = i.Inspector.id;
                    inspector_at_inspection.inspection_id = Inspection.Id;
                    InspectorsAtInspection.Add(new InspectorAtInspectionVM(inspector_at_inspection));
                });

                using (var context = new FestispecEntities())
                {
                    InspectorsAtInspection.ToList().ForEach(i =>
                    {
                        context.Inspectors_at_inspection.Add(i.Inspectors_at_inspection);
                    });
                    context.SaveChanges();
                }

                _main.SetPage("Inspections");
            } else
            {
                // Show wrong input error message
                Console.WriteLine("Wrong input");
            }

        }

        private bool ValidateInput(InspectionVM inspection)
        {
            bool isValid = true;

            isValid = IsDescriptionValid(inspection.Description);
            isValid = IsStartDateTimeInFuture(inspection.Start_date);
            isValid = IsEndDateAfterTheStartDate(inspection.Start_date, inspection.End_date);

            return isValid;
        }

        private bool IsDescriptionValid(string description)
        {
            if (description == null) 
            {
                return false;
            } else if(description.Length < 0)
            {
                return false;
            } else if(description.Length > 500)
            {
                return false;
            }

            return true;
        }

        private bool IsStartDateTimeInFuture(DateTime startDate)
        {
            return startDate > DateTime.Now;
        }

        private bool IsEndDateAfterTheStartDate(DateTime startDate, DateTime endDate)
        {
            return endDate > startDate;
        }

        private void SelectInspector(InspectorsVM inspector)
        {
            Inspectors.Remove(inspector);
            SelectedInspectors.Add(inspector);
        }

        private void DelectInspector(InspectorsVM inspector)
        {
            SelectedInspectors.Remove(inspector);
            Inspectors.Add(inspector);
        }

        private void Debug()
        {
            Console.WriteLine();
        }
    }
}
