using CommonServiceLocator;
using Festispec.ViewModel.Inspections;
using Festispec.ViewModel.Questionnaires;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;

namespace Festispec.ViewModel
{

    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);


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

        public InspectionsViewModel Inspections
        {
            get
            {
                return new InspectionsViewModel(Main);
            }
        }

        public InspectionAddViewModel InspectionAdd
        {
            get
            {
                return new InspectionAddViewModel();
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}