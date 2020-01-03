using Festispec.Domain;
using Festispec.View;
using Festispec.View.Questionnaires;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Windows.Controls;
using System.Windows.Input;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Windows;
using System.Collections.ObjectModel;
using Festispec.Utils;
using Festispec.ViewModel.DataService;

namespace Festispec.ViewModel
{

    public class MainViewModel : ViewModelBase
    {
        private Page _frameContent;

        public ObservableCollection<MenuItem> MenuItems { get; set; }

        public Page FrameContent
        {
            get { return _frameContent; }
            set
            {
                _frameContent = value;
                RaisePropertyChanged("FrameContent");
            }
        }

        private string _pageTitle;

        public string PageTitle
        {
            get { return _pageTitle; }
            set
            {
                _pageTitle = value;
                RaisePropertyChanged("PageTitle");
            }
        }

        public ICommand SetPageCommand { get; set; }

        public ICommand BackCommand { get; set; }

        private Stack<Page> StackNavigator = new Stack<Page>();
        private IDataService _service;

        public MainViewModel(IDataService service)
        {
            _service = service;
            SetPage("Schedule");
            SetMenuItems();
            BackCommand = new RelayCommand(Back, CanGoBack);
            SetPageCommand = new RelayCommand<string>((page) => SetPage(page));
        }

        public void SetPage(string page)
        {

            switch (page)
            {
                case "Schedule":
                    FrameContent = new View.Schedule.Schedule();
                    PageTitle = "Planning";
                    break;
                case "ManageDashboard":
                    FrameContent = new View.Dashboard.ManageDashboard();
                    PageTitle = "Management Dashboard";
                    break;
                case "Festival":
                    FrameContent = new View.FestivalViews.Festivals();
                    PageTitle = "Festival beheer";
                    break;
                case "FestivalInfo":
                    FrameContent = new View.FestivalViews.FestivalInfo();
                    PageTitle = "Festival info";
                    break;
                case "Clients":
                    FrameContent = new View.ClientsViews.Client();
                    PageTitle = "Klanten beheer";
                    break;
                case "ClientInfo":
                    FrameContent = new View.ClientsViews.ClientInfo();
                    PageTitle = "Klanten info";
                    break;
                case "Vragenlijsten":
                    FrameContent = new View.Questionnaires.Questionnaires();
                    PageTitle = "Vragenlijsten";
                    break;
                case "Inspections":
                    FrameContent = new View.Inspections.Inspections();
                    PageTitle = "Inspecties";
                    break;
                case "AddInspection":
                    FrameContent = new View.Inspections.AddInspection();
                    PageTitle = "Inspectie toevoegen";
                    break;
                case "EditInspection":
                    FrameContent = new View.Inspections.EditInspection();
                    PageTitle = "Inspectie toevoegen";
                    break;
                case "Municipality":
                    FrameContent = new View.Municipality.Municipality();
                    PageTitle = "Gemeenten";
                    break;
                case "AddMunicipality":
                    FrameContent = new View.Municipality.AddMunicipality();
                    PageTitle = "Gemeenten toevoegen";
                    break;
                case "AddClient":
                    FrameContent = new View.ClientsViews.AddClients();
                    PageTitle = "Klanten toevoegen";
                    break;
                case "EditClient":
                    FrameContent = new View.ClientsViews.EditClient();
                    PageTitle = "Klanten wijzigen";
                    break;
                case "addFestival":
                    FrameContent = new View.FestivalViews.AddFestival();
                    PageTitle = "Festival toevoegen";
                    break;
                case "EditFestival":
                    FrameContent = new View.FestivalViews.EditFestival();
                    PageTitle = "Festival wijzigingen";
                    break;
                case "Inspectors":
                    FrameContent = new View.Inspectors.Inspectors();
                    PageTitle = "Inspecteurs beheer";
                    break;
                case "AddInspector":
                    FrameContent = new View.Inspectors.AddInspector();
                    PageTitle = "Inspecteur Toevoegen";
                    break;
                case "EditInspector":
                    FrameContent = new View.Inspectors.EditInspector();
                    PageTitle = "Inspecteur bewerken";
                    break;
                case "Templates":
                    FrameContent = new View.Templates.Templates();
                    PageTitle = "Sjablonen";
                    break;
                case "InspectorInfo":
                    FrameContent = new View.Inspectors.InspectorInfo();
                    PageTitle = "Inspecteur informatie";
                    break;
                case "ShowAddContactPerson":
                    FrameContent = new View.ContactPersonsView.AddContactPerson();
                    PageTitle = "Contactpersoon toevoegen";
                    break;
                case "ShowContactPersonInfo":
                    FrameContent = new View.ContactPersonsView.ContactPersonInfo();
                    PageTitle = "Contactpersoon informatie";
                    break;
                case "ShowEditContactPerson":
                    FrameContent = new View.ContactPersonsView.EditContactPerson();
                    PageTitle = "Contactpersoon wijzigen";
                    break;
                case "ContactPersonManagement":
                    FrameContent = new View.ContactPersonsView.ContactPersonsManage();
                    PageTitle = "Contactpersoon beheer";
                    break;
                case "AddContactFestival":
                    FrameContent = new View.ContactPersonsView.AddFestivalContact();
                    PageTitle = "Contactpersoon toevoegen aan festival";
                    break;
                case "Users":
                    FrameContent = new View.Users.Users();
                    PageTitle = "Gebruikers";
                    break;
                case "AddUser":
                    FrameContent = new View.Users.AddUser();
                    PageTitle = "Voeg gebruiker toe";
                    break;
                case "EditUser":
                    FrameContent = new View.Users.EditUser();
                    PageTitle = "Verander gebruiker";
                    break;
                case "Logout":
                    closeWindow();
                    break;
                default:
                    FrameContent = new View.Schedule.Schedule();
                    PageTitle = "Planning";
                    break;
            }

            StackNavigator.Push(FrameContent);
        }

        private void Back()
        {
            if (StackNavigator.Count >= 2)
            {
                StackNavigator.Pop();
                FrameContent = StackNavigator.Peek();
            }
        }

        private bool CanGoBack()
        {
            return StackNavigator.Count >= 2;
        }

        private void closeWindow()
        {
            new LoginWindow().Show();
            Application.Current.Windows[0].Close();
        }

        private void SetMenuItems()
        {
            var userRole = new UserRole(_service.LoggedInUser);

            MenuItems = new ObservableCollection<MenuItem>();
            Console.WriteLine();

            if (userRole.HasUserRole(new string[] {"Admin", "ProjectManager", "Secretariat", "Sales", "Management" }))
            {
                MenuItems.Add(new MenuItem("Planning", "Schedule", "CalendarMonthOutline"));
            }
            if (userRole.HasUserRole(new string[] { "Admin", "Secretariat", "Sales", "ProjectManager" }) || _service.IsOffline)
            {
                MenuItems.Add(new MenuItem("Festivals", "Festival", "Camping"));
            }
            if (userRole.HasUserRole(new string[] { "Admin", "Secretariat", "Sales", "ProjectManager" }))
            {
                MenuItems.Add(new MenuItem("Klanten", "Clients", "AccountBoxOutline"));
            }
            if (userRole.HasUserRole(new string[] { "Admin", "Secretariat", "Sales", "ProjectManager" }))
            {
                MenuItems.Add(new MenuItem("Offerte", "", "FileDocument"));
            }
            if (userRole.HasUserRole(new string[] { "Admin", "Secretariat", "ProjectManager" }))
            {
                MenuItems.Add(new MenuItem("Sjablonen", "Templates", "ArrangeSendBackward"));
            }
            if (userRole.HasUserRole(new string[] { "Admin", "Sales", "Management" }))
            {
                MenuItems.Add(new MenuItem("Dashboard", "ManageDashboard", "MonitorDashboard"));
            }
            if (userRole.HasUserRole(new string[] { "Admin" }))
            {
                MenuItems.Add(new MenuItem("Gebruikers", "Users", "AccountBadgeHorizontalOutline"));
            }
            
            MenuItems.Add(new MenuItem("Uitloggen", "Logout", "ExitToApp"));
        }
    }
}