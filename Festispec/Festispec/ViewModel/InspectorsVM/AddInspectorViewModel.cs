using Festispec.Domain;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Festispec.ViewModel.InspectorsVM
{
    public class AddInspectorViewModel : ViewModelBase
    {
        private InspectorListViewModel _inspectors;

        public InspectorviewModel Inspector { get; set; }
        private String _password;

        public ICommand AddInspectorCommand { get; set; }
        public ICommand CreatePass { get; set; }

        public String Password
        {
            get
            {
                return _password;

            }
            set
            {
                _password = value;
                base.RaisePropertyChanged("Password");
            }
        }
        public AddInspectorViewModel(InspectorListViewModel Inspectors)
        {
            this._inspectors = Inspectors;
            this.Inspector = new InspectorviewModel();

            AddInspectorCommand = new RelayCommand(AddInspectorMethod, CanAddInspector);
            CreatePass = new RelayCommand(GeneratePassword);
        }

        private void AddInspectorMethod()
        {
            Inspector.Active = DateTime.Now;
            _inspectors.Inspectors.Add(Inspector);
            var newuser = new Users();
            newuser.email = Inspector.InspectorFirstName + Inspector.InspectorLastName.Replace(" ", string.Empty);
            newuser.password = CalculateMD5Hash(Password);
           
            using (var context = new FestispecEntities())
            {
                context.Inspectors.Add(Inspector.ToModel());
                //First time savechanges to generate inspector id wich we need before making a new user
                context.SaveChanges();
                var newinspector = context.Inspectors.Attach(Inspector.ToModel());
                newuser.inspector_id = newinspector.id;
                context.Users.Add(newuser);
                
                context.SaveChanges();
            }
            _inspectors.ShowInspectorPage();

        }

        public bool CanAddInspector()
        {
            if(IsMatch())
            {
                return true;
            }
            return false;
        }

        private bool IsMatch()
        {
            if (!IsEmptyField(Inspector.InspectorFirstName) && !IsEmptyField(Inspector.InspectorLastName) && !IsEmptyField(Inspector.PostalCode) && !IsEmptyField(Inspector.Street) && !IsEmptyField(Inspector.Housenumber))
            {
                return true;
            }
            return false;
        }

        private bool IsEmptyField(string input)
        { 
           if (string.IsNullOrEmpty(input))
           {
               return true;
           }
           return false;
        }
        private string CreatePassword(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }
        private void GeneratePassword()
        {
            var passwd = CreatePassword(10);
            Password = passwd;
        }
        public string CalculateMD5Hash(string input)

        {

            // step 1, calculate MD5 hash from input

            MD5 md5 = System.Security.Cryptography.MD5.Create();

            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);

            byte[] hash = md5.ComputeHash(inputBytes);


            // step 2, convert byte array to hex string

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)

            {

                sb.Append(hash[i].ToString("X2"));

            }

            return sb.ToString();

        }


    }
}
