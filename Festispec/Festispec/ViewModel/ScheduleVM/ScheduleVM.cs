using Festispec.Domain;
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

        private int _columns = 4;

        public int Columns
        {
            get { return _columns; }
            set
            {
                if (value <= 31)
                {
                    _columns = value;
                    Properties.Settings.Default.Schedule_Columns = value;
                    Properties.Settings.Default.Save();
                    RaisePropertyChanged("Columns");
                    SetDates();
                }
            }
        }

        private int _amountOfDaysToShow = 7;

        public int AmountOfDaysToShow
        {
            get { return _amountOfDaysToShow; }
            set
            {
                if (value <= 31)
                {
                    _amountOfDaysToShow = value;
                    Properties.Settings.Default.Schedule_AmountOfDaysToShow = value;
                    Properties.Settings.Default.Save();
                    RaisePropertyChanged("AmountOfDaysToShow");
                    SetDates();
                }
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

        public ScheduleVM()
        {
            Schedule = new ObservableCollection<ScheduleColumnVM>();

            MinWeek = new RelayCommand(() => SelectedDate = SelectedDate.AddDays(-7));
            AddWeek = new RelayCommand(() => SelectedDate = SelectedDate.AddDays(7));
            AmountOfDaysToShow = Properties.Settings.Default.Schedule_AmountOfDaysToShow;
            Columns = Properties.Settings.Default.Schedule_Columns;


            SetDates();
        }

        private void SetDates()
        {
            Schedule.Clear();

            for (DateTime date = SelectedDate; date < SelectedDate.AddDays(AmountOfDaysToShow); date = date.AddDays(1))
            {
                Schedule.Add(new ScheduleColumnVM(date));
            }
        }
    }
}
