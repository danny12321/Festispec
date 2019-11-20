using CommonServiceLocator;
using Festispec.ViewModel.ClientVM;
using Festispec.ViewModel.Festival_VMs;
using Festispec.ViewModel.Questionnaires;
using Festispec.ViewModel.MunicipalityVM;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;

namespace Festispec.ViewModel
{

    public class ViewModelLocator
    {
        private InspectorsVM.InspectorListViewModel _inspectors;
        private MunicipalityVM.MunicipalityViewModel _municipality;
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<PopUpViewModel>();
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public LoginViewModel Login
        {
            get
            {
                return new LoginViewModel();
            }
        }

        public PopUpViewModel PopUpWindow
        {
            get
            {
                return new PopUpViewModel();
            }
        }

        public FestivalManagementVM festival
        {
            get
            {
                return new FestivalManagementVM();
            }
        }

        public QuestionnairesViewModel Questionnaires
        {
            get
            {
                return new QuestionnairesViewModel();

            }
        }
        public InspectorsVM.InspectorListViewModel InspectorsVM
        {
            get
            {
                _inspectors = new InspectorsVM.InspectorListViewModel(Main);
                return _inspectors;
            }
        }
        public InspectorsVM.AddInspectorViewModel AddInspector
        {
            get
            {
                
                return new ViewModel.InspectorsVM.AddInspectorViewModel(_inspectors);
            }
        }
        public InspectorsVM.EditInspectorViewModel EditInspector
        {
            get
            {

                return new ViewModel.InspectorsVM.EditInspectorViewModel(_inspectors);
            }
        }

        public MunicipalityViewModel MunicipalityVM
        {
            get
            {
                _municipality = new MunicipalityVM.MunicipalityViewModel(Main);
                return _municipality;
            }
        }



        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}
