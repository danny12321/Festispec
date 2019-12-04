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
        public ICommand ShowAddInspector { get; set; }
        public ICommand ShowEditInspector { get; set; }
        private IDataService _service;

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
        public InspectorListViewModel(MainViewModel main,IDataService dataservice)

        {
            _service = dataservice;
            _mainViewModel = main;
            ShowAddInspector = new RelayCommand(AddInspector);
            ShowEditInspector = new RelayCommand(EditInspector);
            using (var context = new FestispecEntities())
            {

                var inspectors = context.Inspectors.ToList();

                context.SaveChanges();
                Inspectors = new ObservableCollection<InspectorviewModel>(inspectors.Select(i => new InspectorviewModel(i)));


            }
        }
        public void ShowInspectorPage()
        {
            _mainViewModel.SetPage("Inspectors", false);
        }
        public void AddInspector()
        {
            _mainViewModel.SetPage("AddInspector", false);
        }
        public void EditInspector()
        {
            _mainViewModel.SetPage("EditInspector", false);
        }

    }
}
