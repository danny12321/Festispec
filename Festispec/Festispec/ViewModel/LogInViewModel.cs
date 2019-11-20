using Festispec.Domain;
using Festispec.View;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
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
    public class LoginViewModel
    {
        public ICommand LoginCommand { get; set; }

        public AccountsVM.UsersVM _user;
        public AccountsVM.UserRollsVM _roll;
        private Boolean _validated = true;

        public string Email { get; set; }

        public string Password { get; set; }


        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(Login);
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


        private void Login()
        {
            //moet bij release eruit.
            if (Password != null)
            {
                CalculateMD5Hash(Password);
            }
            
            _user = new AccountsVM.UsersVM();
            _roll = new AccountsVM.UserRollsVM();
            _user.email = Email;

            using (var context = new FestispecEntities())
            {
                var password = context.Users.Where(u => u.email == _user.email).ToList();
                //voor testen.
                if(password.Count != 0)
                {
                    if (password[0].password.Equals(CalculateMD5Hash(ToString())))
                    {
                        _validated = true;
                    }
                }

            }
            if (_validated == true)
            {
                if (String.IsNullOrEmpty(Email) == false || String.IsNullOrEmpty(Email) == true)//voor test.
                {
                    _roll.id = 1;
                    _roll.role = "Admin";
                    new BaseWindow().Show();
                    Application.Current.Windows[0].Close();
                }

                else
                {
                    new PopUpWindow().Show();
                }
            }
        }
    }
}
