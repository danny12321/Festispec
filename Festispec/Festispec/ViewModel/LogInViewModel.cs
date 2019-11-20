using Festispec.Domain;
using Festispec.View;
using Festispec.ViewModel.AccountsVM;
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
            if(string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(Email))
            {
                new PopUpWindow().Show();
                return;
            }

            List<Users> user;

            // Voor testen
            if (true)
            {
                using (var context = new FestispecEntities())
                {
                    user = context.Users.ToList();
                }
            }
            else
            {
                using (var context = new FestispecEntities())
                {
                    var hPass = CalculateMD5Hash(Password);
                    user = context.Users.Where(u => (u.email == Email && u.password == hPass)).ToList();
                    Console.WriteLine(hPass);
                }
            }

            if (user.Count > 0)
            {
                // user is ingelogd
                new UsersVM(user[0]);
                new BaseWindow().Show();
                Application.Current.Windows[0].Close();
            }
            else
            {
                // inloggegevens zijn fout
                new PopUpWindow().Show();
            }
        }
    }
}
