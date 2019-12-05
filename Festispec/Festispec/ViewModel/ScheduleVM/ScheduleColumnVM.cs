using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Festispec.Domain;
using Festispec.ViewModel.Inspections;
using GalaSoft.MvvmLight;

namespace Festispec.ViewModel.ScheduleVM
{
    public class ScheduleColumnVM : ViewModelBase
    {
        public ObservableCollection<InspectionVM> Inspections { get; set; }

        private DateTime _date;

        public string Date
        {
            get
            {
                return _date.Date.ToLongDateString();
            }
        }

        public ScheduleColumnVM(DateTime date)
        {
            _date = date;
            RaisePropertyChanged("Date");

            using (var context = new FestispecEntities())
            {
                var inspections = context.Inspections.ToList().Where(i => i.end_date.Date == date.Date).Select(i => new InspectionVM(i));
                Inspections = new ObservableCollection<InspectionVM>(inspections);
            }
        }
    }
}
