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

namespace Festispec.ViewModel.Users
{
    public class UsersVM
    {
        public ObservableCollection<UserVM> Users { get; set; }

        private MainViewModel _main;
        private IDataService _dataService;

        public ICommand NavigateAddUserCommand { get; set; }
        public ICommand NavigateEditUserCommand { get; set; }

        public UsersVM(MainViewModel main, IDataService dataService)
        {
            _main = main;
            _dataService = dataService;

            Users = new ObservableCollection<UserVM>(GetUsers());

            NavigateAddUserCommand = new RelayCommand(NavigateAddUser);
            NavigateEditUserCommand = new RelayCommand<UserVM>(NavigateEditUser);
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
            _main.SetPage("AddUser");
        }

        private void NavigateEditUser(UserVM user)
        {
            _dataService.SelectedUser = user;
            _main.SetPage("EditUser");
        }
    }
}
