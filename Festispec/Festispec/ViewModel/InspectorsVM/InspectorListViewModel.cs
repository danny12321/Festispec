using Festispec.Domain;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel.InspectorsVM
{
    public class InspectorListViewModel : ViewModelBase
    {
        public ObservableCollection<InspectorviewModel> Inspectors { get; set; }
        private InspectorviewModel _selectedInspector;
        public InspectorviewModel SelectedInspector
        {
            get
            {
                return _selectedInspector;
            }
            set
            {
                _selectedInspector = value;
                base.RaisePropertyChanged();
            }
        }
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
