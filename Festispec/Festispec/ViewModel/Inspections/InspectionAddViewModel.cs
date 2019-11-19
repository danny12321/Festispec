using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Festispec.Domain;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Microsoft.Maps.MapControl.WPF;

namespace Festispec.ViewModel.Inspections
{
    public class InspectionAddViewModel : ViewModelBase
    {
        //TODO Make this dynamic
        private int _festivalId = 1;


        public ObservableCollection<InspectorsVM> Inspectors { get; set; }

        public InspectorsVM SelectedInspector { get; set; }

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

        public ICommand TestButton { get; set; }
        public ICommand AddInspectionCommand { get; set; }
        public ICommand SetViewToSelectedPersonCommand { get; set; }

        public InspectionAddViewModel()
        {
            Inspection = new InspectionVM();

            StartDate = DateTime.Now;
            StartTime = DateTime.Now.TimeOfDay;

            EndDate = DateTime.Now;
            EndTime = DateTime.Now.TimeOfDay;

            TestButton = new RelayCommand(Debug);
            AddInspectionCommand = new RelayCommand(AddInspection);
            SetViewToSelectedPersonCommand = new RelayCommand<Map>(SetViewToSelectedPerson);

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

            if (ValidateInput(Inspection))
            {
                using (var context = new FestispecEntities())
                {
                    context.Inspections.Add(Inspection.ToModel());
                    context.SaveChanges();
                }
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

        private void SetViewToSelectedPerson(Map map)
        {
            var longitude = Convert.ToDouble(SelectedInspector.Inspector.longitude);
            var latitude = Convert.ToDouble(SelectedInspector.Inspector.latitude);
            map.SetView(latitude, longitude);
        }

        private void Debug()
        {
            Console.WriteLine();
        }
    }
}
