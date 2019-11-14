using Festispec.Domain;
using Festispec.View;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
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

        public event PropertyChangedEventHandler PropertyChanged;
        private bool _isChecked;
        public AccountsVM.UsersVM _user;
        private Boolean _validated = false;

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

        public string CalculateMD5Hash(string input)
        {
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);
           
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }

        /*
         *TO DO:
         *  Standard User by _isChecked == true
        */

        private void Login(object obj)
        {
            PasswordBox pwBox = obj as PasswordBox;
            Console.WriteLine(pwBox.Password.ToString());
            _user = new AccountsVM.UsersVM();
            _user.email = Email;
            using (var context = new FestispecEntities())
            {
                var password = context.Users.Where(u => u.email == _user.email).ToList();
                Console.WriteLine(CalculateMD5Hash(pwBox.Password.ToString()));
                if(password[0].password.Equals(CalculateMD5Hash(pwBox.Password.ToString())))
                {
                    _validated = true;
                }
            }

            if (_isChecked == false && _validated == false)
            {
                if (String.IsNullOrEmpty(Email) == false)
                {
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
