using Festispec.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel.InspectorsVM
{
    public class InspectorListViewModel
    {
        public ObservableCollection<InspectorviewModel> Inspectors { get; set; }
        private InspectorviewModel _selectedInspector;
        public InspectorListViewModel()
        {
            using (var context = new FestispecEntities())
            {

                var inspectors = context.Inspectors.ToList();

                context.SaveChanges();
                Inspectors = new ObservableCollection<InspectorviewModel>(inspectors.Select(i => new InspectorviewModel(i)));


            }
        }
    }
}
