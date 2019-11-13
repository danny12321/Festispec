using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Festispec.Domain;
using GalaSoft.MvvmLight;

namespace Festispec.ViewModel.FestivalVM
{
    public class FestivalVM : ViewModelBase
    {
        private Festivals _festival;

        public FestivalVM(Festivals festivals)
        {
            this._festival = festivals;
        }

        //properties van tabel toevoegen


        internal Festivals ToModel()
        {
            return _festival;
        }
    }
}
