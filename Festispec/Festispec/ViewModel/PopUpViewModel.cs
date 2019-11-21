using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Festispec.ViewModel
{
    public class PopUpViewModel
    {

        public ICommand OkCommand { get; set; }

        public PopUpViewModel()
        {
            OkCommand = new RelayCommand<Window>(Ok);
        }

        private void Ok(Window window)
        {
            window.Close();
        }
    }
}