using Festispec.View.FestivalViews;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Festispec.ViewModel.Festival_VMs
{
    public class FestivalManagementVM : ViewModelBase
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

        public FestivalManagementVM()
        {
            SetPageCommand = new RelayCommand<string>(SetPage);
        }

        public void SetPage(string page)
        {
            switch (page)
            {
                case "AddFestival":
                    FrameContent = new AddFestival();
                    PageTitle = "Festival toevoegen";
                    break;
                    /*
                case "EditFestival":
                    FrameContent = new EditFestival();
                    PageTitle = "Planning";
                    break;
                    */
                default:
                    FrameContent = new Festivals();
                    PageTitle = "Festival beheer";
                    break;
            }
        }
    }
}
