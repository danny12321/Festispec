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

namespace Festispec.ViewModel.Users
{
    public class EditUserVM
    {
        private MainViewModel _main;
        private IDataService _serviceProvider;
        private List<UserRollVM> _userRoles;
        private int _userId;

        public ObservableCollection<UserRollVM> Rolles { get; set; }

        public UserVM User { get; set; }

        public ICommand EditUserCommand { get; set; }

        public EditUserVM(MainViewModel main, IDataService serviceProvider)
        {
            _main = main;
            _serviceProvider = serviceProvider;

            Rolles = new ObservableCollection<UserRollVM>(GetRolles());

            _userId = _serviceProvider.SelectedUser.Id;

            User = GetUser().ToList().First();

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

                var newRoles = context.Rolls
                    .Where(r => newRolsIds.Contains(r.id))
                    .ToList();

                user.Rolls.Clear();
                foreach (var newRole in newRoles)
                    user.Rolls.Add(newRole);

                context.SaveChanges();
            }

        }
    }
}
