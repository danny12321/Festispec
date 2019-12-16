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

namespace Festispec.ViewModel
{

    public class MainViewModel : ViewModelBase
    {
        private Page _frameContent;

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

        private Stack<string> StackNavigator = new Stack<string>();

        public MainViewModel()
        {
            SetPage("Home", false);
            BackCommand = new RelayCommand(Back, CanGoBack);
            SetPageCommand = new RelayCommand<string>((page) => SetPage(page, false));
        }

        public void SetPage(string page, bool navigator)
        {

            switch (page)
            {
                case "Home":
                    FrameContent = new Home();
                    PageTitle = "Home";
                    break;
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
                case "Vragenlijsten TEMP":
                    FrameContent = new View.Questionnaires.Questionnaires();
                    PageTitle = "Vragenlijsten";
                    break;
                case "Inspections":
                    FrameContent = new View.Inspections.Inspections();
                    PageTitle = "Inspections";
                    break;
                case "AddInspection":
                    FrameContent = new View.Inspections.AddInspection();
                    PageTitle = "Add Inspection";
                    break;
                case "EditInspection":
                    FrameContent = new View.Inspections.EditInspection();
                    PageTitle = "Edit Inspection";
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
                case "InspectorInfo":
                    FrameContent = new View.Inspectors.InspectorInfo();
                    PageTitle = "Inspecteur informatie";
                    break;
                case "ShowAddContactPerson":
                    FrameContent = new View.ContactPersonsView.AddContactPerson();
                    PageTitle = "contactpersoon toevoegen";
                    break;
                case "ShowContactPersonInfo":
                    FrameContent = new View.ContactPersonsView.ContactPersonInfo();
                    PageTitle = "contactpersoon informatie";
                    break;
                case "ShowEditContactPerson":
                    FrameContent = new View.ContactPersonsView.EditContactPerson();
                    PageTitle = "contactpersoon wijzigen";
                    break;
                case "ContactPersonManagement":
                    FrameContent = new View.ContactPersonsView.ContactPersonsManage();
                    PageTitle = "Contactpersoon beheer";
                    break;
                case "AddContactFestival":
                    FrameContent = new View.ContactPersonsView.AddFestivalContact();
                    PageTitle = "Contactpersoon toevoegen aan festival";
                    break;
                case "Logout":
                    closeWindow();
                    break;
                default:
                    FrameContent = new Home();
                    PageTitle = "Home";
                    break;
            }

            if (!navigator)
            {
                StackNavigator.Push(page);
            }
        }

        private void Back()
        {
            if (StackNavigator.Count >= 2)
            {
                StackNavigator.Pop();
                SetPage(StackNavigator.Peek(), true);
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
    }
}