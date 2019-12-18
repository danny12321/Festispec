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
        public bool HasUserRole(UsersVM user, string[] roles)
        {
            var hasRole = false;

            user.ToModel().Rolls.ToList().ForEach(u =>
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
