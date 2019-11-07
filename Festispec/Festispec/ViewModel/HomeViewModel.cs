using Festispec.View;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Festispec.ViewModel
{
    class HomeViewModel : ViewModelBase
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
        public ICommand NavigateTestPageCommand { get; set; }


        public HomeViewModel()
        {
            FrameContent = new Home();

            NavigateHomeCommand = new RelayCommand(NavigateHome);
            NavigateTestPageCommand = new RelayCommand(NavigateTestPage);
        }

        private void NavigateHome()
        {
            FrameContent = new Home();
        }

        private void NavigateTestPage()
        {
            FrameContent = new TestPage();
        }
    }
}
