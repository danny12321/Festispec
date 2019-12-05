using Festispec.Domain;
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
    public class UsersVM
    {
        public ObservableCollection<UserVM> Users { get; set; }

        public MainViewModel _main;

        public ICommand NavigateAddUserCommand { get; set; }

        public UsersVM(MainViewModel main)
        {
            Users = new ObservableCollection<UserVM>(GetUsers());

            _main = main;

            NavigateAddUserCommand = new RelayCommand(NavigateAddUser);
        }

        private IEnumerable<UserVM> GetUsers()
        {
            using (var context = new FestispecEntities())
            {
                return context.Users.ToList().Select(i => new UserVM(i));
            }
        }

        private void NavigateAddUser()
        {
            _main.SetPage("AddUser", false);
        }
    }
}
