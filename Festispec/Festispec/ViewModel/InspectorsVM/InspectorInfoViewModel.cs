using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Festispec.Domain;
using Festispec.ViewModel.DataService;
using Festispec.ViewModel.Inspections;
using GalaSoft.MvvmLight;

namespace Festispec.ViewModel.InspectorsVM
{
    public class InspectorInfoViewModel : ViewModelBase
    {
        private IDataService _service;
        public ObservableCollection<InspectionVM> Inspections { get; set; }

        public InspectorviewModel SelectedInspector
        {
            get { return _service.SelectedInspector; }
        }

        public InspectionVM SelectedInspection
        {
            get { return _service.SelectedInspection; }
            set
            {
                _service.SelectedInspection = value;
                RaisePropertyChanged();
            }
        }
        public InspectorInfoViewModel(IDataService dataService)
        {
            this._service = dataService;

            using (var context = new FestispecEntities())
            {
                var inspectorinspectionid = context.Inspectors_at_inspection.ToList().Where(i => i.inpector_id == SelectedInspector.InspectorId).Select(e => e.inspection_id);
                var inspections = context.Inspections.ToList()
                             .Select(e => new InspectionVM(e)).Where(e => inspectorinspectionid.Contains(e.Id));

                Inspections = new ObservableCollection<InspectionVM>(inspections);
            }
        }
    }
}
