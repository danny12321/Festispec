using Festispec.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel.AccountsVM
{
    public class UsersVM
    {
        private Users _user;
        public UsersVM()
        {
            _user = new Users();
        }
        public UsersVM(Users user)
        {
            _user = user;
        }
        public int id
        {
            get { return _user.id; }
            set { _user.id = value; }
        }
        public string email
        {
            get { return _user.email; }
            set { _user.email = value; }
        }
        public string password
        {
            get { return _user.password; }
            set { _user.password = value; }
        }
        public Nullable<int> inspector_id
        {
            get { return _user.inspector_id; }
            set { _user.inspector_id = value; }
        }
        internal Users ToModel()
        {
            return _user;
        }
    }
}
