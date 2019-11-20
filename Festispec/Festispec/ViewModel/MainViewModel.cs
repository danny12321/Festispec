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
                    FrameContent = new Schedule();
                    PageTitle = "Planning";
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
    }
}