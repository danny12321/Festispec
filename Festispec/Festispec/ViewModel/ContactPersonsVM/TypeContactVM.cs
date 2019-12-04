using Festispec.Domain;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel.ContactPersonsVM
{
    public class TypeContactVM : ViewModelBase
    {
        private Type_contacts _typeContact;

        public TypeContactVM(Type_contacts typeContact)
        {
            _typeContact = typeContact;
        }

        public TypeContactVM()
        {
            _typeContact = new Type_contacts();
        }

        public int Id
        {
            get { return _typeContact.id; }
            set { value = _typeContact.id; RaisePropertyChanged(); }
        }

        public string Type
        {
            get { return _typeContact.type; }
            set { value = _typeContact.type; RaisePropertyChanged(); }
        }

        internal Type_contacts ToModel()
        {
            return _typeContact;
        }
    }
}
