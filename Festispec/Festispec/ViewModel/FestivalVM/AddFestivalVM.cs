using Festispec.Domain;
using Festispec.ViewModel.ClientVM;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Festispec.ViewModel.Festival_VMs
{
    public class AddFestivalVM : ViewModelBase
    {
        private ClientInfoVM _clients;
        private TimeSpan _StartTime;
        private DateTime _StartDateBegin;
        private TimeSpan _EndTime;
        private DateTime _EndDateEnd;

        public TimeSpan StartTime
        {
            get { return _StartTime; }
            set { _StartTime = value; }
        }

        public DateTime StartDateBegin
        {
            get { return _StartDateBegin; }
            set { _StartDateBegin = value; }
        }

        public TimeSpan EndTime
        {
            get { return _EndTime; }
            set { _EndTime = value; }
        }

        public DateTime EndDateEnd
        {
            get { return _EndDateEnd; }
            set { _EndDateEnd = value; }
        }

        public ICommand AddFestivalCommand { get; set; }

        public FestivalVM.FestivalVM Festival { get; set; }

        public AddFestivalVM(ClientInfoVM client)
        {
            this._clients = client;
            Festival = new FestivalVM.FestivalVM();

            AddFestivalCommand = new RelayCommand(AddFestivalMethod, CanAddFestival);

            Festival.ClientId = _clients.SelectedClient.ClientId;
            Festival.MunicipalityId = 1;
            _StartDateBegin = DateTime.Now;
            _StartTime = DateTime.Now.TimeOfDay;
            _EndDateEnd = DateTime.Now;
            _EndTime = DateTime.Now.TimeOfDay;
        }

        private bool CanAddFestival()
        {
            return true;
        }

        private void AddFestivalMethod()
        {
            Festival.StartDate = (StartDateBegin + StartTime);
            Festival.EndDate = (EndDateEnd + EndTime);
            _clients.Festivals.Add(Festival);

            using (var context = new FestispecEntities())
            {
                context.Festivals.Add(Festival.ToModel());
                context.SaveChanges();
            }
            _clients.ShowClientPage();
        }
    }
}
