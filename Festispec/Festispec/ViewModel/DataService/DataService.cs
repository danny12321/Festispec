﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Festispec.ViewModel.ClientVM;
using Festispec.ViewModel.ContactPersonsVM;
using Festispec.ViewModel.FestivalVM;
using Festispec.ViewModel.Questionnaires;
using Festispec.ViewModel.Inspections;
using Festispec.ViewModel.InspectorsVM;
using Festispec.ViewModel.Users;


namespace Festispec.ViewModel.DataService
{
    public class DataService : IDataService
    {
        public ClientsVM SelectedClient { get; set; }
        public FestivalVM.FestivalVM SelectedFestival { get; set; }
        public QuestionnairesViewModel SelectedQuestionnaire { get; set; }
        public ContactPersonVM SelectedContactPerson { get; set; }
        public InspectionVM SelectedInspection { get; set; }
        public InspectorviewModel SelectedInspector { get; set; }
        public UserVM SelectedUser { get; set; }
        public bool IsOffline { get; set; } = false;
    }
}
