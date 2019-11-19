using Festispec.Domain;
using Festispec.View.FestivalViews;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Festispec.ViewModel.Festival_VMs
{
    public class FestivalManagementVM : ViewModelBase
    {

        public ObservableCollection<FestivalVM.FestivalVM> FestivalList;

        public ICommand ShowFestival;
        public ICommand ShowAddInspection;

        public FestivalManagementVM()
        {
            using (var context = new FestispecEntities())
            {
                var festival = context.Festivals.ToList()
                             .Select(e => new FestivalVM.FestivalVM(e));

                FestivalList = new ObservableCollection<FestivalVM.FestivalVM>(festival);
            }
        }

    }
}
