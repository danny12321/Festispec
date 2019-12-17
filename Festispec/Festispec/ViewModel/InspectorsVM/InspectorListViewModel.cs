using Festispec.Domain;
using Festispec.ViewModel.DataService;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Festispec.ViewModel.InspectorsVM
{
    public class InspectorListViewModel : ViewModelBase
    {
        public ObservableCollection<InspectorviewModel> Inspectors { get; set; }
        private InspectorviewModel _selectedInspector;
        private MainViewModel _mainViewModel;
        private IDataService _service;
        public ICommand ShowAddInspector { get; set; }
        public ICommand ShowEditInspector { get; set; }
        public ICommand ShowInspectorInfo { get; set; }

        public InspectorviewModel SelectedInspector
        {
            get
            {
                return _service.SelectedInspector;
            }
            set
            {
                _service.SelectedInspector = value;
                base.RaisePropertyChanged();
            }
        }
        public InspectorListViewModel(MainViewModel main, IDataService dataService)
        {
            _mainViewModel = main;
            _service = dataService;
            ShowAddInspector = new RelayCommand(AddInspector);
            ShowEditInspector = new RelayCommand(EditInspector);
            ShowInspectorInfo = new RelayCommand(InspectorInfo);
            using (var context = new FestispecEntities())
            {
                var inspectors = context.Inspectors.ToList();

                context.SaveChanges();
                Inspectors = new ObservableCollection<InspectorviewModel>(inspectors.Select(i => new InspectorviewModel(i)));
            }
        }
        public void ShowInspectorPage()
        {
            _mainViewModel.SetPage("Inspectors");
        }
        public void AddInspector()
        {
            _mainViewModel.SetPage("AddInspector");
        }
        public void EditInspector()
        {
            _mainViewModel.SetPage("EditInspector");
        }

        public void InspectorInfo()
        {
            _mainViewModel.SetPage("InspectorInfo", false);
        }

    }
}
