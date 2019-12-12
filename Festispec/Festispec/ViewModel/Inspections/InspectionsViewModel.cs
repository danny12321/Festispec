using Festispec.Domain;
using Festispec.ViewModel.DataService;
using GalaSoft.MvvmLight.CommandWpf;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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

        public string NoInspections { get { return Inspections.Count > 0 ? null : "Nog geen inspecties"; } }

        public InspectionsViewModel(MainViewModel main, IDataService service)
        {
            _main = main;
            _service = service;
            _festivalId = service.SelectedFestival.FestivalId;

            NavigateEditInspectionCommand = new RelayCommand<InspectionVM>(NavigateEditInspection);

            if (_service.IsOffline)
            {
                GetDataFromJson();
            } else
            {
                NavigateAddInspectionCommand = new RelayCommand(NavigateAddInspection);

                using (var context = new FestispecEntities())
                {
                    var inspections = context.Inspections.ToList().Where(i => i.festival_id == _festivalId).Select(i => new InspectionVM(i));
                    Inspections = new ObservableCollection<InspectionVM>(inspections);
                }

                CreateOfflineInspectionData();
            }

        }

        private void GetDataFromJson()
        {
            // Get old JSON
            string fileName = "Inspections.json";
            string path = Path.Combine(Environment.CurrentDirectory, @"Offline\", fileName);
            string jsonInspectionsData = File.ReadAllText(path);

            // Parse JSON to object
            JArray parsedInspectionJson = JArray.Parse(jsonInspectionsData);

            Inspections = new ObservableCollection<InspectionVM>();

            for (int i = 0; i < parsedInspectionJson.Count; i++)
            {
                if (parsedInspectionJson[i]["Festival_id"].Value<int>() == _festivalId)
                {
                    var inspectionVM = new InspectionVM();
                    inspectionVM.Id = parsedInspectionJson[i]["Id"].Value<int>();
                    inspectionVM.Description = parsedInspectionJson[i]["Description"].ToString();
                    inspectionVM.Start_date = parsedInspectionJson[i]["Start_date"].Value<DateTime>();
                    inspectionVM.End_date = parsedInspectionJson[i]["End_date"].Value<DateTime>();
                    inspectionVM.Finished = parsedInspectionJson[i]["Finished"].Value<DateTime?>();
                    inspectionVM.Festival_id = parsedInspectionJson[i]["Festival_id"].Value<int>();

                    Inspections.Add(inspectionVM);
                }
            }
        }

        private void NavigateAddInspection()
        {
            _main.SetPage("AddInspection", false);
        }

        private void NavigateEditInspection(InspectionVM inspection)
        {
            _service.SelectedInspection = inspection;
            _main.SetPage("EditInspection", false);
        }
        private void CreateOfflineInspectionData()
        {
            // Get old JSON
            string fileName = "Inspections.json";
            string path = Path.Combine(Environment.CurrentDirectory, @"Offline\", fileName);

            string fileContent;

            // If it exsitst add to it
            if (File.Exists(path))
            {
                string jsonInspectionsData = File.ReadAllText(path);

                // Parse JSON to object
                JArray parsedInspectionJson = JArray.Parse(jsonInspectionsData);

                // Check wich inspection are allready in json file
                // And add the one who is not in there
                for (int i = 0; i < Inspections.Count; i++)
                {
                    bool hasInspection = false;
                    for (int j = 0; j < parsedInspectionJson.Count; j++)
                    {
                        if (Inspections[i].Id == int.Parse(parsedInspectionJson[j]["Id"].ToString()))
                        {
                            hasInspection = true;
                            break;
                        }
                    }
                    if (!hasInspection)
                    {
                        parsedInspectionJson.Add(JObject.FromObject(Inspections[i]));
                    }
                }

                fileContent = parsedInspectionJson.ToString();
            } else // If it not exist, make a new one
            {
                fileContent = JsonConvert.SerializeObject(Inspections);
            }

            // Write file
            using (StreamWriter outputFile = new StreamWriter(path))
            {
                outputFile.WriteLine(fileContent);
            }

        }
    }
}
