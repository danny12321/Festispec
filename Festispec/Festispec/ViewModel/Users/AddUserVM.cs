﻿using Festispec.Domain;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Festispec.ViewModel.Users
{
    public class AddUserVM : ViewModelBase
    {
        public UserVM User { get; set; }

        public ObservableCollection<UserRollVM> Rolles { get; set; }
        public string ErrorMessage { get; set; }

        public ICommand AddUserCommand { get; set; }

        private MainViewModel _main;

        public AddUserVM(MainViewModel main)
        {
            _main = main;

            User = new UserVM();
            Rolles = new ObservableCollection<UserRollVM>(GetRolles());

            AddUserCommand = new RelayCommand(AddUser);
        }

        private IEnumerable<UserRollVM> GetRolles()
        {
            using (var context = new FestispecEntities())
            {
                return context.Rolls.ToList().Select(i => new UserRollVM(i));
            }
        }

        private void AddUser()
        {
            if (InputIsCorrect())
            {
                using (var context = new FestispecEntities())
                {
                    Rolles.ToList().ForEach(r =>
                    {
                        if (r.Checked)
                        {
                            User.ToModel().Rolls.Add(r.ToModel());
                            context.Rolls.Attach(r.ToModel());
                        }
                    });

                    context.Users.Add(User.ToModel());

                    context.SaveChanges();
                }

                _main.SetPage("Users", false);
            } else
            {
                // Incorrect input -- Show error message
                ErrorMessage = "Email moet ingevult zijn\nWachtwoord moet minimaal 6 karakters hebben";
                RaisePropertyChanged("ErrorMessage");
            }
        }

        private bool InputIsCorrect()
        {
            if (!EmailIsCorrect())
            {
                return false;
            }

            if (!PasswordIsCorrect())
            {
                return false;
            }

            return true;
        }

        private bool EmailIsCorrect()
        {
            if (User.Email == null)
            {
                return false;
            }
            if (User.Email.Length < 0)
            {
                return false;
            }

            return true;
        }

        private bool PasswordIsCorrect()
        {
            if (User.Password == null)
            {
                return false;
            }
            if (User.Password.Length < 6)
            {
                return false;
            }

            return true;
        }
    }
}
