using CommonServiceLocator;
using Festispec.ViewModel.Questionnaires;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;

namespace Festispec.ViewModel
{

    public class ViewModelLocator
    {
        private InspectorsVM.InspectorListViewModel _inspectors;
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            _inspectors = new InspectorsVM.InspectorListViewModel();

            SimpleIoc.Default.Register<MainViewModel>();
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
                return _inspectors;
            }
        }
        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}