using Festispec.View;
using Festispec.View.Questionnaires;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Controls;
using System.Windows.Input;

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
                RaisePropertyChanged();
            }
        }

        public ICommand NavigateHomeCommand { get; set; }
        public ICommand NavigateScheduleCommand { get; set; }
        public ICommand NavigateQuestionnairesCommand { get; set; }

        public MainViewModel()
        {
            FrameContent = new Home();

            NavigateHomeCommand = new RelayCommand(NavigateHome);
            NavigateScheduleCommand = new RelayCommand(NavigateSchedule);
            NavigateQuestionnairesCommand = new RelayCommand(NavigateQuestionnaires);
        }

        private void NavigateHome()
        {
            System.Console.WriteLine("Dit is geweldig");
            FrameContent = new Home();
        }

        private void NavigateSchedule()
        {
            System.Console.WriteLine("Dit is geweldig");
            FrameContent = new Schedule();
        }

        private void NavigateQuestionnaires()
        {
            FrameContent = new Questionnaires();
        }
    }
}