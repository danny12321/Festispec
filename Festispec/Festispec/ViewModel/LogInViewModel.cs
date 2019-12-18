using Festispec.Domain;
using Festispec.View;
using Festispec.ViewModel.AccountsVM;
using Festispec.ViewModel.DataService;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
        public ICommand GoOfflineCommand { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        private IDataService _dataService;
        public bool ShowOfflineButton { get; set; }
      
        public LoginViewModel(IDataService dataService)
        {
            _dataService = dataService;

            LoginCommand = new RelayCommand(Login);
            GoOfflineCommand = new RelayCommand(GoOffline);
            ShowOfflineButton = false;

            if (HasInternetConnection() && HasOfflineData())
            {
                ShowOfflineButton = true;
            }
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

            List<Domain.Users> user;

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

            CreateOfflineUserData();
        }

        private bool HasInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead("http://google.com/generate_204"))
                    return true;
            }
            catch
            {
                return false;
            }
        }

        private bool HasOfflineData()
        {
            string fileName = "User.json";
            string path = Path.Combine(Environment.CurrentDirectory, @"Offline\", fileName);

            return File.Exists(path);
        }

        private void CreateOfflineUserData()
        {
            // TODO When login is up to date make user dynamic
            string fileContent = JsonConvert.SerializeObject(new {Id = 1, Email = "email@mail.com", Firstname = "Testname", Lastname = "Testlastname", Roles = new string[] { "Admin" } }); ;
            
            string fileName = "User.json";
            string path = Path.Combine(Environment.CurrentDirectory, @"Offline\", fileName);

            Directory.CreateDirectory("Offline");

            using (StreamWriter outputFile = new StreamWriter(path))
            {
                outputFile.WriteLine(fileContent);
            }
        }

        private void GoOffline()
        {
            _dataService.IsOffline = true;

            // Get old JSON
            string fileName = "User.json";
            string path = Path.Combine(Environment.CurrentDirectory, @"Offline\", fileName);
            string jsonInspectionsData = File.ReadAllText(path);

            // Parse JSON to object
            JObject parsedUserJson = JObject.Parse(jsonInspectionsData);

            // Nothing is done with this yet
            var userVM = new UsersVM();
            userVM.id = parsedUserJson["Id"].Value<int>();
            userVM.email = parsedUserJson["Email"].ToString();

            new BaseWindow().Show();
            Application.Current.Windows[0].Close();
        }

    }
}
