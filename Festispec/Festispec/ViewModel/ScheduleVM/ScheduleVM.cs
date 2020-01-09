using Festispec.Domain;
using Festispec.ViewModel.DataService;
using Festispec.ViewModel.Inspections;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Festispec.ViewModel.ScheduleVM
{
    public class ScheduleVM : ViewModelBase
    {
        public ObservableCollection<ScheduleColumnVM> Schedule { get; set; }

        private int _amountOfDaysToShow = 7;

        public int AmountOfDaysToShow
        {
            get { return _amountOfDaysToShow; }
            set
            {
                _amountOfDaysToShow = value;
                Properties.Settings.Default.Schedule_AmountOfDaysToShow = value;
                Properties.Settings.Default.Save();
                RaisePropertyChanged("AmountOfDaysToShow");
                SetDates();
            }
        }

        private DateTime _selectedDate = DateTime.Now;

        public DateTime SelectedDate
        {
            get
            {
                return _selectedDate;
            }
            set
            {
                _selectedDate = value;
                RaisePropertyChanged("SelectedDate");
                SetDates();
            }
        }

        public ICommand MinWeek { get; set; }

        public ICommand AddWeek { get; set; }

        public ICommand SetWeekCommand { get; set; }

        public ICommand SetMonthCommand { get; set; }

        private IDataService _service;

        private MainViewModel _main;

        public ScheduleVM(MainViewModel main, IDataService service)
        {
            Schedule = new ObservableCollection<ScheduleColumnVM>();

            _main = main;
            _service = service;

            MinWeek = new RelayCommand(() => SelectedDate = SelectedDate.AddDays(-AmountOfDaysToShow));
            AddWeek = new RelayCommand(() => SelectedDate = SelectedDate.AddDays(AmountOfDaysToShow));
            SetWeekCommand = new RelayCommand(() => AmountOfDaysToShow = 7);
            SetMonthCommand = new RelayCommand(() => AmountOfDaysToShow = 31);

            AmountOfDaysToShow = Properties.Settings.Default.Schedule_AmountOfDaysToShow;

            SetDates();
        }

        private void SetDates()
        {
            Schedule.Clear();

            for (DateTime date = SelectedDate; date < SelectedDate.AddDays(AmountOfDaysToShow); date = date.AddDays(1))
            {
                Schedule.Add(new ScheduleColumnVM(date, _main, _service));
            }
        }

    }
}
