﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Festispec.Domain;
using Festispec.Utils;
using Festispec.ViewModel.DataService;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Microsoft.Maps.MapControl.WPF;
using Newtonsoft.Json.Linq;

namespace Festispec.ViewModel.Inspections
{
    public class InspectionAddViewModel : ViewModelBase
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
        
        // Same as Festival only difference is the check if there are coordinates
        public FestivalVM FestivalMaps { get; set; }

        public InspectionVM Inspection { get; set; }
        public InspectionVM InspectionMaps { get; set; }

        private DateTime _startDate;
        public DateTime StartDate { get { return _startDate; } set { _startDate = value; EndDate = value; RaisePropertyChanged("EndDate"); CheckInspectorAvailability(); } }

        private TimeSpan _startTime;
        public TimeSpan StartTime { get { return _startTime; } set { _startTime = value; EndTime = TimeSpan.FromMinutes(value.TotalMinutes + 60); RaisePropertyChanged("EndTime"); CheckInspectorAvailability(); } }

        private DateTime _endDate;
        public DateTime EndDate { get { return _endDate; } set { _endDate = value; CheckInspectorAvailability(); } }

        private TimeSpan _endTime;
        public TimeSpan EndTime { get { return _endTime; } set { _endTime = value; CheckInspectorAvailability(); } }

        public DateTime EndDateTimeCombined
        {
            get { return EndDate + EndTime; }
        }

        public DateTime StartDateTimeCombined {
            get { return StartDate + StartTime; }
        }

        private MainViewModel _main;

        public string ErrorMessage { get; set; }

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
            SelectedInspectorsMaps = new ObservableCollection<InspectorsVM>();
            InspectorsMaps = new ObservableCollection<InspectorsVM>();
            Inspection = new InspectionVM();

            StartDate = DateTime.Now;
            StartTime = DateTime.Now.TimeOfDay;
            StartTime = TimeSpan.FromMinutes(Math.Round(StartTime.TotalMinutes));

            EndDate = DateTime.Now;
            EndTime = DateTime.Now.TimeOfDay;
            EndTime = TimeSpan.FromMinutes(Math.Round(StartTime.TotalMinutes) + 60);

            TestButton = new RelayCommand(Debug);
            AddInspectionCommand = new RelayCommand(AddInspection);
            SelectInspectorCommand = new RelayCommand<InspectorsVM>(SelectInspector);
            DelectInspectorCommand = new RelayCommand<InspectorsVM>(DelectInspector);

            using (var context = new FestispecEntities())
            {
                //Get inspectors
                var inspectors = context.Inspectors.Include("Inspectors_availability").ToList().Select(i => new InspectorsVM(i));

                Inspectors = new ObservableCollection<InspectorsVM>(inspectors);
                   
                Festival = new FestivalVM(context.Festivals.ToList().First(f => f.id == _festivalId));
            }

            //Calc Travel Time
            if (_useUpAllFreeApiRequestsForTravelCalculationAndLetThierryPayForIt)
            {
                Inspectors.ToList().ForEach(i => {
                    var timespan = CalculateRouteDurationForInspector(i).GetAwaiter().GetResult();
                    i.TravelTime = timespan;
                });
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
        }

        private void AddInspection()
        {
            // Combining all values in here instead of binding directly to the view because the start and end datetime is separate
            Inspection.Start_date = StartDateTimeCombined;
            Inspection.End_date = EndDateTimeCombined;

            Inspection.Festival_id = _festivalId;

            InspectorsAtInspection = new ObservableCollection<InspectorAtInspectionVM>();

            if (ValidateInput(Inspection))
            {
                using (var context = new FestispecEntities())
                {
                    context.Inspections.Add(Inspection.ToModel());
                    context.SaveChanges();
                    _inspetionId = Inspection.ToModel().id;
                }

                SelectedInspectors.ToList().ForEach(i => {
                    var inspector_at_inspection = new Inspectors_at_inspection();
                    inspector_at_inspection.inpector_id = i.id;
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

                CreateOfflineInspectionData();
                _main.SetPage("Inspections");

            } else
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

        private async Task<TimeSpan> CalculateRouteDurationForInspector(InspectorsVM inspector)
        {
            RouteDurationCalculator routeDurationCalculator = new RouteDurationCalculator();
            return await routeDurationCalculator.CalculateRoute(inspector.longitude + "," + inspector.latitude, Festival.Festivals.longitude + "," + Festival.Festivals.latitude).ConfigureAwait(false);
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

            // Check wich inspection are allready in json file
            // And add the one who is not in there
            parsedInspectionJson.Add(JObject.FromObject(Inspection));

            parsedInspectionJson[parsedInspectionJson.Count - 1]["notSelectedInspectors"] = JArray.FromObject(Inspectors);
            parsedInspectionJson[parsedInspectionJson.Count - 1]["selectedInspectors"] = JArray.FromObject(SelectedInspectors);

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
            if (Inspectors != null)
            {

                Inspectors.ToList().ForEach(i =>
                {
                    if (InspectorIsNotAvailable(i))
                    {
                        i.Available = "Niet beschikbaar";
                        i.RaisePropertyChanged("Available");
                    } else
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
