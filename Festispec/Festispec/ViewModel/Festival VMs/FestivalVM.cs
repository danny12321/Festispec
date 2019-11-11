using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Festispec.Domain;
using GalaSoft.MvvmLight;

namespace Festispec.ViewModel.Festival_VMs
{
    public class FestivalVM : ViewModelBase
    {
        private Festival _festival;

        public FestivalVM(Festival festivals)
        {
            this._festival = festivals;
        }

        //properties van tabel toevoegen


        internal Festival ToModel()
        {
            return _festival;
        }
    }
}
