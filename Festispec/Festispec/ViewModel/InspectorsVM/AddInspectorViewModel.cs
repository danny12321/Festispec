using Festispec.Domain;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Festispec.ViewModel.InspectorsVM
{
    public class AddInspectorViewModel
    {
        private InspectorListViewModel _inspectors;

        public InspectorviewModel Inspector { get; set; }

        public ICommand AddInspectorCommand { get; set; }

        public AddInspectorViewModel(InspectorListViewModel Inspectors)
        {
            this._inspectors = Inspectors;
            this.Inspector = new InspectorviewModel();

            AddInspectorCommand = new RelayCommand(AddInspectorMethod, CanAddInspector);
        }

        private void AddInspectorMethod()
        {
            Inspector.Active = DateTime.Now;
            _inspectors.Inspectors.Add(Inspector);

            using (var context = new FestispecEntities())
            {
                context.Inspectors.Add(Inspector.ToModel());
                context.SaveChanges();
            }
            _inspectors.ShowInspectorPage();

        }

        public bool CanAddInspector()
        {
            if(IsMatch())
            {
                return true;
            }
            return false;
        }

        private bool IsMatch()
        {
            if (!IsEmptyField(Inspector.InspectorFirstName) && !IsEmptyField(Inspector.InspectorLastName) && !IsEmptyField(Inspector.PostalCode) && !IsEmptyField(Inspector.Street) && !IsEmptyField(Inspector.Housenumber))
            {
                return true;
            }
            return false;
        }

        private bool IsEmptyField(string input)
        { 
           if (string.IsNullOrEmpty(input))
           {
               return true;
           }
           return false;
        }

    }
}
