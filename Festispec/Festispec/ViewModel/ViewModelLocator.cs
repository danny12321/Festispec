using CommonServiceLocator;
using Festispec.ViewModel.Festival_VMs;
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

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}