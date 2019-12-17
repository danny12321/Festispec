using Festispec.Domain;
using Festispec.Utils;
using Festispec.ViewModel.DataService;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Festispec.ViewModel.Inspections
{
    public class InspectionEditViewModel : ViewModelBase
    {
        private bool _useUpAllFreeApiRequestsForTravelCalculationAndLetThierryPayForIt = false;

        private int _festivalId;
        private int _inspetionId;

        public ObservableCollection<InspectorsVM> Inspectors { get; set; }
        public ObservableCollection<InspectorsVM> InspectorsMaps { get; set; }

        public ObservableCollection<InspectorAtInspectionVM> InspectorsAtInspection { get; set; }

        public ObservableCollection<InspectorsVM> SelectedInspectors { get; set; }
        public ObservableCollection<InspectorsVM> SelectedInspectorsMaps { get; set; }

        public FestivalVM Festival { get; set; }
        public FestivalVM FestivalMaps { get; set; }

        public InspectionVM Inspection { get; set; }

        private DateTime _startDate;
        public DateTime StartDate { get { return _startDate; } set { _startDate = value; EndDate = value; } }

        private TimeSpan _startTime;
        public TimeSpan StartTime { get { return _startTime; } set { _startTime = value; EndTime = value; } }

        public DateTime EndDate { get; set; }
        public TimeSpan EndTime { get; set; }

        public DateTime EndDateTimeCombined
        {
            get { return EndDate + EndTime; }
        }

        public DateTime StartDateTimeCombined
        {
            get { return StartDate + StartTime; }
        }

        private MainViewModel _main;

        public string ErrorMessage { get; set; }

        public ICommand TestButton { get; set; }
        public ICommand EditInspectionCommand { get; set; }
        public ICommand SetViewToSelectedPersonCommand { get; set; }
        public ICommand SelectInspectorCommand { get; set; }
        public ICommand DelectInspectorCommand { get; set; }
        public ICommand DeleteInspectionCommand { get; set; }

        private IDataService _service;

        public InspectionEditViewModel(MainViewModel main, IDataService service)
        {
            _main = main;
            _service = service;

            _festivalId = service.SelectedFestival.FestivalId;
            _inspetionId = service.SelectedInspection.Id;

            TestButton = new RelayCommand(Debug);
            EditInspectionCommand = new RelayCommand(EditInspection);
            SelectInspectorCommand = new RelayCommand<InspectorsVM>(SelectInspector);
            DelectInspectorCommand = new RelayCommand<InspectorsVM>(DelectInspector);

            using (var context = new FestispecEntities())
            {
                //Get inspectors
                var inspectors = context.Inspectors.ToList()
                .Select(i => new InspectorsVM(i));

                Inspectors = new ObservableCollection<InspectorsVM>(inspectors);
                SelectedInspectors = new ObservableCollection<InspectorsVM>();

                //Used for posistion of festival in bing maps
                Festival = new FestivalVM(context.Festivals.ToList().First(f => f.id == _festivalId));

                //Get the inspection
                Inspection = new InspectionVM(context.Inspections.ToList().First(i => i.id == _inspetionId));
                StartDate = Inspection.Start_date;
                StartTime = Inspection.Start_date.TimeOfDay;

                EndDate = Inspection.End_date;
                EndTime = Inspection.End_date.TimeOfDay;

                var questionnaires = context.Questionnaires.Where(q => q.inspection_id == _inspetionId).ToList().Select(q => new QuestionnairesViewModel(q));
                Questionnaires = new ObservableCollection<QuestionnairesViewModel>(questionnaires);

                //Calc Travel Time
                if (_useUpAllFreeApiRequestsForTravelCalculationAndLetThierryPayForIt)
                {
                    Inspectors.ToList().ForEach(i => {
                        var timespan = CalculateRouteDurationForInspector(i).GetAwaiter().GetResult();
                        i.TravelTime = timespan;
                    });
                }

                context.Inspectors_at_inspection.ToList().ForEach(i =>
                {
                    if (i.inspection_id == _inspetionId)
                    {
                        var tempInspector = Inspectors.ToList().First(j => j.Inspector.id == i.inpector_id);
                        SelectedInspectors.Add(tempInspector);
                        Inspectors.Remove(tempInspector);
                    }
                });

                RaisePropertyChanged();
            }
        }

        private void EditInspection()
        {
            // Combining all values in here instead of binding directly to the view because the start and end datetime is separate
            Inspection.Start_date = StartDateTimeCombined;
            Inspection.End_date = EndDateTimeCombined;

            // TODO Make festival id dynamic
            Inspection.Festival_id = _festivalId;

            InspectorsAtInspection = new ObservableCollection<InspectorAtInspectionVM>();

            if (ValidateInput(Inspection))
            {
                SelectedInspectors.ToList().ForEach(i => {
                    var inspector_at_inspection = new Inspectors_at_inspection();
                    inspector_at_inspection.inpector_id = i.Inspector.id;
                    inspector_at_inspection.inspection_id = _inspetionId;
                    InspectorsAtInspection.Add(new InspectorAtInspectionVM(inspector_at_inspection));
                });

                using (var context = new FestispecEntities())
                {
                    context.Entry(Inspection.ToModel()).State = EntityState.Modified;

                    context.Inspectors_at_inspection.RemoveRange(context.Inspectors_at_inspection.Where(i => i.inspection_id == _inspetionId));

                    InspectorsAtInspection.ToList().ForEach(i =>
                    {
                        context.Inspectors_at_inspection.Add(i.Inspectors_at_inspection);
                    });

                    context.SaveChanges();
                }

                _main.SetPage("Inspections");
            }
            else
            {
                // Show wrong input error message
                ErrorMessage = "Beschrijving mag niet leeg zijn\nStart datum en tijd moet in de toekomst liggen\nEind datum en tijd moet na de start zijn";
                RaisePropertyChanged("ErrorMessage");
                Console.WriteLine("Wrong input");
            }

        }

        private bool ValidateInput(InspectionVM inspection)
        {
            if (!IsDescriptionValid(inspection.Description))
            {
                return false;
            }
            if(IsStartDateTimeInFuture(inspection.Start_date))
            {
                return false;
            }
            if(IsEndDateAfterTheStartDate(inspection.Start_date, inspection.End_date))
            {
                return false;
            }

            return true;
        }

        private bool IsDescriptionValid(string description)
        {
            if (description == null)
            {
                return false;
            }
            else if (description.Length < 0)
            {
                return false;
            }
            else if (description.Length > 500)
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

            if (InspectorsMaps.Contains(inspector))
            {
                InspectorsMaps.Remove(inspector);
                SelectedInspectorsMaps.Add(inspector);
            }
        }

        private void DelectInspector(InspectorsVM inspector)
        {
            SelectedInspectors.Remove(inspector);
            Inspectors.Add(inspector);

            if (SelectedInspectorsMaps.Contains(inspector))
            {
                SelectedInspectorsMaps.Remove(inspector);
                InspectorsMaps.Add(inspector);
            }
        }

        private void DeleteInspection()
        {
            var questionnaire = new Domain.Questionnaires() { inspection_id = _inspetionId };

            using (var context = new FestispecEntities())
            {
                context.Questionnaires.Add(questionnaire);
                context.SaveChanges();
            }

            var questionnaireVM = new QuestionnairesViewModel(questionnaire);
            Questionnaires.Add(questionnaireVM);
        }

        private async Task<TimeSpan> CalculateRouteDurationForInspector(InspectorsVM inspector)
        {
            using (var context = new FestispecEntities())
            {
                _service.SelectedQuestionnaire = new QuestionnairesViewModel(context.Questionnaires.FirstOrDefault(q => q.id == questionnaire.Id));
                _main.SetPage("Vragenlijsten");
            }
        }

        private void Debug()
        {
            Console.WriteLine();
        }
    }
}
