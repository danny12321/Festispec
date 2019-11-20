using Festispec.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel.AccountsVM
{
    public class UserRollsVM
    {
        private Rolls _roll;
        public UserRollsVM()
        {
            _roll = new Rolls();
        }
        public UserRollsVM(Rolls roll)
        {
            _roll = roll;
        }

        public int id
        {
            get { return _roll.id; }
            set { _roll.id = value; }
        }
        public string role
        {
            get { return _roll.role; }
            set { _roll.role = value; }
        }

        internal Rolls ToModel()
        {
            return _roll;
        }
    }
}