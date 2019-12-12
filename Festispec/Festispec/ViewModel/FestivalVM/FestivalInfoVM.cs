using Festispec.Domain;
using Festispec.ViewModel.DataService;
using Festispec.ViewModel.FestivalVMs;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Festispec.ViewModel.FestivalVM
{
    public class FestivalInfoVM : ViewModelBase
    {
        private IDataService _service;
        private FestivalManagementVM _festivals;
        private MainViewModel _main;

        public ObservableCollection<FestivalVM> Festivals { get; set; }

        public FestivalVM SelectedFestival
        {
            get { return _service.SelectedFestival; }
            set
            {
                _service.SelectedFestival = value;
                RaisePropertyChanged();
            }
        }

        public FestivalInfoVM(MainViewModel main, IDataService service, FestivalManagementVM festivals)
        {
            this._main = main;
            _service = service;
            this._festivals = festivals;

            using (var context = new FestispecEntities())
            {
                var festival = context.Festivals.ToList()
                             .Select(e => new FestivalVM(e));

                Festivals = new ObservableCollection<FestivalVM>(festival);
            }

        }



    }
}
