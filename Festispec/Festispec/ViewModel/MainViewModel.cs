using Festispec.Domain;
using Festispec.View;
using Festispec.View.Questionnaires;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Controls;
using System.Windows.Input;
using System.Linq;
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

        public MainViewModel()
        {
            SetPage("Home");

            SetPageCommand = new RelayCommand<string>(SetPage);
        }

        public void SetPage(string page)
        {
            switch (page)
            {
                case "Home":
                    FrameContent = new Home();
                    PageTitle = "Home"; 
                    break;
                case "Schedule":
                    FrameContent = new Schedule();
                    PageTitle = "Planning";
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
                case "Logout":
                    closeWindow();
                    break;
                default:
                    FrameContent = new Home();
                    PageTitle = "Home";
                    break;
            }
        }

        private void closeWindow()
        {
            new LoginWindow().Show();
            Application.Current.Windows[0].Close();
        }
    }
}