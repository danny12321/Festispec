using CommonServiceLocator;
using Festispec.ViewModel.ClientVM;
using Festispec.ViewModel.DataService;
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
            SimpleIoc.Default.Register<IDataService, DataService.DataService>();
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
                    _clientManageVM = new ClientManageVM(Main, ServiceLocator.Current.GetInstance<IDataService>());
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

        public ClientInfoVM clientinfo
        {
            get
            {
                return new ClientInfoVM(ServiceLocator.Current.GetInstance<IDataService>(), _clientManageVM);
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