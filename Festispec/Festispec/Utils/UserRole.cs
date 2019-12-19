using Festispec.ViewModel.AccountsVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.Utils
{
    public class UserRole
    {
        private UsersVM _user;

        public UserRole(UsersVM user)
        {
            _user = user;
        }

        public bool HasUserRole(string[] roles)
        {
            var hasRole = false;

            if (_user == null)
            {
                return false;
            }

            _user.ToModel().Rolls.ToList().ForEach(u =>
            {
                for (int i = 0; i < roles.Length; i++)
                {
                    if (u.role == roles[i])
                    {
                        hasRole = true;
                    }
                }
            });

            return hasRole;
        }
    }
}
