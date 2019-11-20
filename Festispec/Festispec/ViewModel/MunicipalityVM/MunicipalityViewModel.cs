using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Festispec.ViewModel.MunicipalityVM
{
    public class MunicipalityViewModel : ViewModelBase
    {
        private MainViewModel _mainViewModel;
        public ICommand ShowAddMunicipality { get; set; }

        public MunicipalityViewModel(MainViewModel main)
        {
            _mainViewModel = main;
            ShowAddMunicipality = new RelayCommand(AddMunicipality);
            
        }

        public void AddMunicipality()
        {
            Console.WriteLine("woop");
            _mainViewModel.SetPage("AddMunicipality");
        }
    }


}
