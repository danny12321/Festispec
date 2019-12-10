using Festispec.Domain;
using Festispec.ViewModel.DataService;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Data.Entity.Migrations;
using System.Data.Entity;
using Festispec.Utils;
using GalaSoft.MvvmLight;

namespace Festispec.ViewModel.Users
{
    public class EditUserVM : ViewModelBase
    {
        private MainViewModel _main;
        private IDataService _serviceProvider;
        private List<UserRollVM> _userRoles;
        private int _userId;

        public ObservableCollection<UserRollVM> Rolles { get; set; }

        public UserVM User { get; set; }

        public string ErrorMessage { get; set; }

        public ICommand EditUserCommand { get; set; }

        public EditUserVM(MainViewModel main, IDataService serviceProvider)
        {
            _main = main;
            _serviceProvider = serviceProvider;

            Rolles = new ObservableCollection<UserRollVM>(GetRolles());

            _userId = _serviceProvider.SelectedUser.Id;

            User = GetUser().ToList().First();

            User.Password = "";

            _userRoles = GetUserRoles().ToList();

            EditUserCommand = new RelayCommand(EditUser);

            GiveUserRolles();
        }

        private IEnumerable<UserVM> GetUser()
        {
            using (var context = new FestispecEntities())
            {
                return context.Users.Where(u => u.id == _userId).ToList().Select(i => new UserVM(i));
            }
        }

        private IEnumerable<UserRollVM> GetRolles()
        {
            using (var context = new FestispecEntities())
            {
                return context.Rolls.ToList().Select(i => new UserRollVM(i));
            }
        }

        private IEnumerable<UserRollVM> GetUserRoles()
        {
            using (var context = new FestispecEntities())
            {
                return context.Rolls.Where(r => r.Users.Any(u => u.id == User.Id)).ToList().Select(r => new UserRollVM(r));
            }
        }

        private void GiveUserRolles()
        {
            _userRoles.ForEach(ur =>
            {
                Rolles.ToList().ForEach(r => 
                {
                    if (ur.Id == r.Id)
                    {
                        r.Checked = true;
                    }
                });
            });
        }

        private void EditUser()
        {
            if (InputIsCorrect())
            {
                List<int> newRolsIds = new List<int>();
                Rolles.ToList().ForEach(r =>
                {
                    if (r.Checked)
                    {
                        newRolsIds.Add(r.Id);
                    }
                });

                using (var context = new FestispecEntities())
                {
                    var user = context.Users.Include("Rolls")
                    .Single(u => u.id == User.Id);

                    user.email = User.Email;

                    if (PasswordIsCorrect())
                    {
                        var passwordHasher = new PasswordHasher();
                        user.password = passwordHasher.ComputeSha256Hash(User.Password);
                    }

                    var newRoles = context.Rolls
                        .Where(r => newRolsIds.Contains(r.id))
                        .ToList();

                    user.Rolls.Clear();
                    foreach (var newRole in newRoles)
                        user.Rolls.Add(newRole);

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

            return true;
        }

        private bool EmailIsCorrect()
        {
            if (User.Email == null)
            {
                return false;
            }
            if (User.Email.Length < 1)
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
