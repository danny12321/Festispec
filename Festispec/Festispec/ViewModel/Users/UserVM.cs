using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel.Users
{
    public class UserVM
    {
        private Domain.Users _user { get; set; }

        public UserVM(Domain.Users user)
        {
            _user = user;
        }

        public int Id { 
            get { return _user.id; }
            set { _user.id = value;  }
        }
        public string Email
        {
            get { return _user.email; }
            set { _user.email = value; }
        }
        public string Password
        {
            get { return _user.password; }
            set { _user.password = value; }
        }

        public Domain.Users ToModel()
        {
            return _user;
        }
    }
}
