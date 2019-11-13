using Festispec.Domain;
using Festispec.View;
using Festispec.View.Festival_Views;
using Festispec.View.Questionnaires;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Controls;
using System.Windows.Input;
using System.Linq;

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
                    FrameContent = new View.Festival_Views.Festivals();
                    PageTitle = "Festival beheer";
                    break;
                case "Clients":
                    FrameContent = new View.ClientsViews.Client();
                    PageTitle = "Klanten beheer";
                    break;
                case "Vragenlijsten TEMP":
                    FrameContent = new View.Questionnaires.Questionnaires();
                    PageTitle = "Vragenlijsten";
                    break;
                case "AddClient":
                    FrameContent = new View.ClientsViews.AddClients();
                    PageTitle = "Klanten toevoegen";
                    break;
                default:
                    FrameContent = new Home();
                    PageTitle = "Home";
                    break;
            }
        }
    }
}