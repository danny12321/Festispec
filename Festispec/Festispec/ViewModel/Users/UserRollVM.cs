using Festispec.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel.Users
{
    public class UserRollVM
    {
        private Rolls _roll;

        public int Id
        {
            get { return _roll.id; }
            set { _roll.id = value; }
        }

        public string Roll
        {
            get { return _roll.role; }
            set { _roll.role = value; }
        }

        public bool Checked { get; set; }

        public UserRollVM(Rolls roll)
        {
            _roll = roll;
        }

        public UserRollVM()
        {
            _roll = new Rolls();
        }

        public Rolls ToModel()
        {
            return _roll;
        }
    }
}
