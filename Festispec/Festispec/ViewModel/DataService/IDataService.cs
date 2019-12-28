﻿using Festispec.ViewModel.ClientVM;
using Festispec.ViewModel.FestivalVM;
using Festispec.ViewModel.Questionnaires;
using Festispec.ViewModel.ContactPersonsVM;
using Festispec.ViewModel.Inspections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Festispec.ViewModel.InspectorsVM;

namespace Festispec.ViewModel.DataService
{
    public interface IDataService
    {
        ClientsVM SelectedClient { get; set; }

        FestivalVM.FestivalVM SelectedFestival { get; set; }
        QuestionnairesViewModel SelectedQuestionnaire { get; set; }
        ContactPersonVM SelectedContactPerson { get; set; }
        InspectionVM SelectedInspection { get; set; }
        InspectorviewModel SelectedInspector { get; set; }
        ViewModel.Users.UserVM SelectedUser { get; set; }
        bool IsOffline { get; set; }
    }
}
