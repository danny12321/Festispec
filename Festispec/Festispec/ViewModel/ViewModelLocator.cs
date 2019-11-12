using CommonServiceLocator;
using Festispec.ViewModel.ClientVM;
using Festispec.ViewModel.Festival_VMs;
using Festispec.ViewModel.Questionnaires;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;

namespace Festispec.ViewModel
{

    public class ViewModelLocator
    {
        private ClientManageVM _clientManageVM;

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

        public ClientManageVM client
        {
            get
            {
                if(_clientManageVM == null)
                {
                    _clientManageVM = new ClientManageVM(Main);
                }

                return _clientManageVM;
            }
        }

        public AddClientsVM addclient
        {
            get
            {
                return new AddClientsVM(_clientManageVM);
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