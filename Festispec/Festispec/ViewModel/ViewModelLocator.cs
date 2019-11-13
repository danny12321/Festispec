using CommonServiceLocator;
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
