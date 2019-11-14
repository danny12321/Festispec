using Festispec.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel.Inspections
{
    public class InspectionsViewModel
    {
        public ObservableCollection<InspectionVM> Inspections;

        public InspectionsViewModel()
        {
            using (var context = new FestispecEntities())
            {

            }
        }
    }
}
