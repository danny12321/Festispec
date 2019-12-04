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

        private string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }


        private void Login()
        {
            var autoLogin = true;

            if ((string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(Email)) && !autoLogin)
            {
                new PopUpWindow().Show();
                return;
            }

            List<Users> user;

            // Voor testen
            if (autoLogin)
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
                    var hPass = ComputeSha256Hash(Password);
                    Console.WriteLine(hPass);
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
