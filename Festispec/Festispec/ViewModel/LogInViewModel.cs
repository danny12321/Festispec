using Festispec.View;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace Festispec.ViewModel
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public ICommand LoginCommand { get; set; }

        private bool _isChecked;

        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsChecked
        {
            get { return _isChecked; }
            set
            {
                _isChecked = value; 
                OnPropertyChanged("IsChecked"); 
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public string Email { get; set; }

        public string Password { get; set; }

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand<object>(Login);
        }

        /*
         *TO DO:
         *  Inlog email checken DB
         *  Password checken DB
         *  Standard User by _isChecked == true
        */

        private void Login(object obj)
        {
            PasswordBox pwBox = obj as PasswordBox;
            Console.WriteLine(pwBox.Password.ToString());

            if (_isChecked == false)
            {
                if (String.IsNullOrEmpty(Email) == false)
                {
                    //Console.WriteLine($"Login {Email} {Password}");
                    new BaseWindow().Show();
                    Application.Current.Windows[0].Close();
                }

                else
                {
                    new PopUpWindow().Show();
                }
            }
            else
            {
                new BaseWindow().Show();
                Application.Current.Windows[0].Close();
            }
  
        }
    }
}
