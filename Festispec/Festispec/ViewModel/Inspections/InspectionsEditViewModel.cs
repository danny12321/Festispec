using Festispec.Domain;
using Festispec.ViewModel.Questionnaires;
using Festispec.Utils;
using Festispec.ViewModel.DataService;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.IO;
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

        public ObservableCollection<QuestionnairesViewModel> Questionnaires { get; set; }

        public ObservableCollection<InspectorsVM> SelectedInspectorsMaps { get; set; }

        public FestivalVM Festival { get; set; }
        public FestivalVM FestivalMaps { get; set; }

        public InspectionVM Inspection { get; set; }

        private DateTime _startDate;
        public DateTime StartDate { get { return _startDate; } set { _startDate = value; EndDate = value; CheckInspectorAvailability(); } }

        private TimeSpan _startTime;
        public TimeSpan StartTime { get { return _startTime; } set { _startTime = value; EndTime = value; CheckInspectorAvailability(); } }

        private DateTime _endDate;
        public DateTime EndDate { get { return _endDate; } set { _endDate = value; CheckInspectorAvailability(); } }
        private TimeSpan _endTime;
        public TimeSpan EndTime { get { return _endTime; } set { _endTime = value; CheckInspectorAvailability(); } }

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

        public ICommand AddQuestionnaireCommand { get; set; }
        public ICommand OpenQuestionnaireCommand { get; set; }
        public ICommand DeleteQuestionnaireCommand { get; set; }
        public ICommand Report { get; set; }
        public ICommand DeleteInspectionCommand { get; set; }


        private IDataService _service;

        public InspectionEditViewModel(MainViewModel main, IDataService service)
        {
            Console.WriteLine("EDITT");
            _main = main;
            _service = service;

            _festivalId = service.SelectedFestival.FestivalId;
            _inspetionId = service.SelectedInspection.Id;
            Report = new RelayCommand(ShowReport);
            TestButton = new RelayCommand(Debug);

            if (_service.IsOffline)
            {
                GetFromJsonData();
            }
            else
            {
                EditInspectionCommand = new RelayCommand(EditInspection);
                SelectInspectorCommand = new RelayCommand<InspectorsVM>(SelectInspector);
                DelectInspectorCommand = new RelayCommand<InspectorsVM>(DelectInspector);
                DeleteInspectionCommand = new RelayCommand(DeleteInspection);
                AddQuestionnaireCommand = new RelayCommand(AddQuestionnaire);
                OpenQuestionnaireCommand = new RelayCommand<QuestionnairesViewModel>(OpenQuestionnaire);
                DeleteQuestionnaireCommand = new RelayCommand<QuestionnairesViewModel>(DeleteQuestionnaire);

                using (var context = new FestispecEntities())
                {
                    //Get inspectors
                    var inspectors = context.Inspectors.Include("Inspectors_availability").ToList()
                        .Select(i => new InspectorsVM(i));

                    Inspectors = new ObservableCollection<InspectorsVM>(inspectors);
                    SelectedInspectors = new ObservableCollection<InspectorsVM>();
                    SelectedInspectorsMaps = new ObservableCollection<InspectorsVM>();
                    InspectorsMaps = new ObservableCollection<InspectorsVM>();

                    //Used for posistion of festival in bing maps
                    Festival = new FestivalVM(context.Festivals.ToList().First(f => f.id == _festivalId));

                    //Get the inspection
                    Inspection = new InspectionVM(context.Inspections.ToList().First(i => i.id == _inspetionId));
                    StartDate = Inspection.Start_date;
                    StartTime = Inspection.Start_date.TimeOfDay;

                    EndDate = Inspection.End_date;
                    EndTime = Inspection.End_date.TimeOfDay;


                    //Calc Travel Time
                    if (_useUpAllFreeApiRequestsForTravelCalculationAndLetThierryPayForIt)
                    {
                        Inspectors.ToList().ForEach(i => {
                            var timespan = CalculateRouteDurationForInspector(i).GetAwaiter().GetResult();
                            i.TravelTime = timespan;
                        });
                    }

                    var questionnaires = context.Questionnaires.Where(q => q.inspection_id == _inspetionId).ToList().Select(q => new QuestionnairesViewModel(q));
                    Questionnaires = new ObservableCollection<QuestionnairesViewModel>(questionnaires);

                    context.Inspectors_at_inspection.ToList().ForEach(i =>
                    {
                        if (i.inspection_id == _inspetionId)
                        {
                            var tempInspector = Inspectors.ToList().First(j => j.id == i.inpector_id);

                            if (tempInspector.HasPos)
                            {
                                SelectedInspectorsMaps.Add(tempInspector);
                                InspectorsMaps.Remove(tempInspector);
                            }
                            SelectedInspectors.Add(tempInspector);
                            Inspectors.Remove(tempInspector);
                        }
                    });

                    RaisePropertyChanged();
                }

                Inspectors.ToList().ForEach(i => {
                    if (i.HasPos)
                    {
                        InspectorsMaps.Add(i);
                    }
                });

                if (Festival.HasPos)
                {
                    FestivalMaps = Festival;
                }

                CreateOfflineInspectionData();
            }
        }

        private void GetFromJsonData()
        {
            // Get old JSON
            string fileName = "Inspections.json";
            string path = Path.Combine(Environment.CurrentDirectory, @"Offline\", fileName);
            string jsonInspectionsData = File.ReadAllText(path);

            // Parse JSON to object
            JArray parsedInspectionJson = JArray.Parse(jsonInspectionsData);

            JObject inspectionJson = null;

            // Get selected inspection
            for (int i = 0; i < parsedInspectionJson.Count; i++)
            {
                if (parsedInspectionJson[i]["Id"].Value<int>() == _inspetionId)
                {
                    inspectionJson = JObject.Parse(parsedInspectionJson[i].ToString());
                }
            }

            Inspection = new InspectionVM();

            Inspection.Id = inspectionJson["Id"].Value<int>();
            Inspection.Description = inspectionJson["Description"].ToString();
            Inspection.Start_date = inspectionJson["Start_date"].Value<DateTime>();
            Inspection.End_date = inspectionJson["End_date"].Value<DateTime>();
            Inspection.Finished = inspectionJson["Finished"].Value<DateTime?>();
            Inspection.Festival_id = inspectionJson["Festival_id"].Value<int>();

            Inspectors = new ObservableCollection<InspectorsVM>();

            if (inspectionJson["notSelectedInspectors"] != null)
            {
                JArray notSelectedInspectors = JArray.Parse(inspectionJson["notSelectedInspectors"].ToString());
                for (int i = 0; i < notSelectedInspectors.Count; i++)
                {
                    var inspectorVM = new InspectorsVM();
                    inspectorVM.id = notSelectedInspectors[i]["id"].Value<int>();
                    inspectorVM.name = notSelectedInspectors[i]["name"].ToString();
                    inspectorVM.lastname = notSelectedInspectors[i]["lastname"].ToString();
                    inspectorVM.Available = notSelectedInspectors[i]["Available"].ToString();
                    inspectorVM.birthday = notSelectedInspectors[i]["birthday"].Value<DateTime>();
                    inspectorVM.email = notSelectedInspectors[i]["email"].ToString();
                    inspectorVM.postalcode = notSelectedInspectors[i]["postalcode"].ToString();
                    inspectorVM.country = notSelectedInspectors[i]["country"].ToString();
                    inspectorVM.city = notSelectedInspectors[i]["city"].ToString();
                    inspectorVM.street = notSelectedInspectors[i]["street"].ToString();
                    inspectorVM.housenumber = notSelectedInspectors[i]["housenumber"].ToString();
                    inspectorVM.phone = notSelectedInspectors[i]["phone"].ToString();
                    inspectorVM.active = notSelectedInspectors[i]["active"].Value<DateTime>();
                    inspectorVM.latitude = notSelectedInspectors[i]["latitude"].ToString();
                    inspectorVM.longitude = notSelectedInspectors[i]["longitude"].ToString();

                    Inspectors.Add(inspectorVM);
                }
            }

            SelectedInspectors = new ObservableCollection<InspectorsVM>();

            if (inspectionJson["selectedInspectors"] != null)
            {

                JArray selectedInspectors = JArray.Parse(inspectionJson["selectedInspectors"].ToString());
                for (int i = 0; i < selectedInspectors.Count; i++)
                {
                    var inspectorVM = new InspectorsVM();
                    inspectorVM.id = selectedInspectors[i]["id"].Value<int>();
                    inspectorVM.name = selectedInspectors[i]["name"].ToString();
                    inspectorVM.lastname = selectedInspectors[i]["lastname"].ToString();
                    inspectorVM.birthday = selectedInspectors[i]["birthday"].Value<DateTime>();
                    inspectorVM.Available = selectedInspectors[i]["Available"].ToString();
                    inspectorVM.email = selectedInspectors[i]["email"].ToString();
                    inspectorVM.postalcode = selectedInspectors[i]["postalcode"].ToString();
                    inspectorVM.country = selectedInspectors[i]["country"].ToString();
                    inspectorVM.city = selectedInspectors[i]["city"].ToString();
                    inspectorVM.street = selectedInspectors[i]["street"].ToString();
                    inspectorVM.housenumber = selectedInspectors[i]["housenumber"].ToString();
                    inspectorVM.phone = selectedInspectors[i]["phone"].ToString();
                    inspectorVM.active = selectedInspectors[i]["active"].Value<DateTime>();
                    inspectorVM.latitude = selectedInspectors[i]["latitude"].ToString();
                    inspectorVM.longitude = selectedInspectors[i]["longitude"].ToString();

                    SelectedInspectors.Add(inspectorVM);
                }
            }

            StartDate = Inspection.Start_date;
            StartTime = Inspection.Start_date.TimeOfDay;

            EndDate = Inspection.End_date;
            EndTime = Inspection.End_date.TimeOfDay;
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
                SelectedInspectors.ToList().ForEach(i =>
                {
                    var inspector_at_inspection = new Inspectors_at_inspection();
                    inspector_at_inspection.inpector_id = i.id;
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

                CreateOfflineInspectionData();
                _main.SetPage("Inspections");
            }
            else
            {
                // Show wrong input error message
                ErrorMessage = "Beschrijving mag niet leeg zijn\nStart datum en tijd moet in de toekomst liggen\nEind datum en tijd moet na de start zijn";
                RaisePropertyChanged("ErrorMessage");
            }

        }

        private bool ValidateInput(InspectionVM inspection)
        {

            if (!IsDescriptionValid(inspection.Description))
            {
                return false;
            }
            if (!IsStartDateTimeInFuture(inspection.Start_date))
            {
                return false;
            }
            if (!IsEndDateAfterTheStartDate(inspection.Start_date, inspection.End_date))
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
            //Does not work problem with on delete cascade in database :(
            using (var context = new FestispecEntities())
            {
                var inspection = new Festispec.Domain.Inspections { id = _inspetionId };
                context.Inspections.Attach(inspection);
                context.Inspections.Remove(inspection);
                context.SaveChanges();
            }

            _main.SetPage("Inspections");
        }
        public void ShowReport()
        {
            _main.SetPage("ReportPage");
        }
        private async Task<TimeSpan> CalculateRouteDurationForInspector(InspectorsVM inspector)
        {
            RouteDurationCalculator routeDurationCalculator = new RouteDurationCalculator();
            return await routeDurationCalculator.CalculateRoute(inspector.longitude + "," + inspector.latitude, Festival.Festivals.longitude + "," + Festival.Festivals.latitude).ConfigureAwait(false);
        }

        private void AddQuestionnaire()
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

        public void OpenQuestionnaire(QuestionnairesViewModel questionnaire)
        {
            using (var context = new FestispecEntities())
            {
                _service.SelectedQuestionnaire = new QuestionnairesViewModel(context.Questionnaires.FirstOrDefault(q => q.id == questionnaire.Id));
                _main.SetPage("Vragenlijsten");
            }
        }


        private void DeleteQuestionnaire(QuestionnairesViewModel questionnaire)
        {
            using (var context = new FestispecEntities())
            {
                context.Questionnaires.Attach(questionnaire.Questionnaire);
                context.Questionnaires.Remove(questionnaire.Questionnaire);
                context.SaveChanges();

                Questionnaires.Remove(questionnaire);
            }
        }

        private void Debug()
        {
            Console.WriteLine();
        }

        private void CreateOfflineInspectionData()
        {
            // Get old JSON
            string fileName = "Inspections.json";
            string path = Path.Combine(Environment.CurrentDirectory, @"Offline\", fileName);
            string jsonInspectionsData = File.ReadAllText(path);

            // Parse JSON to object
            JArray parsedInspectionJson = JArray.Parse(jsonInspectionsData);

            // Change the active inspection
            for (int i = 0; i < parsedInspectionJson.Count; i++)
            {
                if (int.Parse(parsedInspectionJson[i]["Id"].ToString()) == _inspetionId)
                {
                    parsedInspectionJson[i] = JObject.FromObject(Inspection);
                    parsedInspectionJson[i]["notSelectedInspectors"] = JArray.FromObject(Inspectors);
                    parsedInspectionJson[i]["selectedInspectors"] = JArray.FromObject(SelectedInspectors);
                }
            }

            // Save JSON
            string fileContent = parsedInspectionJson.ToString();

            Directory.CreateDirectory("Offline");

            using (StreamWriter outputFile = new StreamWriter(path))
            {
                outputFile.WriteLine(fileContent);
            }

        }

        private void CheckInspectorAvailability()
        {
            if (_service.IsOffline)
            {
                return;
            }

            if (Inspectors != null)
            {
                Inspectors.ToList().ForEach(i =>
                {
                    if (InspectorIsNotAvailable(i))
                    {
                        i.Available = "Niet beschikbaar";
                        i.RaisePropertyChanged("Available");
                    }
                    else
                    {
                        i.Available = "Beschikbaar";
                        i.RaisePropertyChanged("Available");
                    }
                });
            }

            if (SelectedInspectors != null)
            {

                SelectedInspectors.ToList().ForEach(i =>
                {
                    if (InspectorIsNotAvailable(i))
                    {
                        i.Available = "Niet beschikbaar";
                        i.RaisePropertyChanged("Available");
                    }
                    else
                    {
                        i.Available = "Beschikbaar";
                        i.RaisePropertyChanged("Available");
                    }
                });
            }
        }

        private bool InspectorIsNotAvailable(InspectorsVM inspector)
        {
            bool returnValue = false;

            inspector.ToModel().Inspectors_availability.ToList().ForEach(ia =>
            {
                if (HasAvailability(ia))
                {
                    returnValue = true;
                }
            });

            return returnValue;
        }

        private bool HasAvailability(Inspectors_availability availability)
        {
            return availability.start_date < StartDateTimeCombined && availability.end_date > EndDateTimeCombined;
        }
    }
}
