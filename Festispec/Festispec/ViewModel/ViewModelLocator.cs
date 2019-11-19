using CommonServiceLocator;
using Festispec.ViewModel.ClientVM;
using Festispec.ViewModel.DataService;
using Festispec.ViewModel.Festival_VMs;
using Festispec.ViewModel.FestivalVM;
using Festispec.ViewModel.FestivalVMs;
using Festispec.ViewModel.Questionnaires;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;

namespace Festispec.ViewModel
{

    public class ViewModelLocator
    {
        private ClientManageVM _clientManageVM;
        private ClientInfoVM _clientInfoVM;
        private FestivalManagementVM _festivalManagementVM;
        private FestivalInfoVM _festivalInfoVM;

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
                if (_festivalManagementVM == null)
                {
                    _festivalManagementVM = new FestivalManagementVM(Main, ServiceLocator.Current.GetInstance<IDataService>());
                }
                return _festivalManagementVM;
            }
        }

        public FestivalInfoVM festivalinfo
        {
            get
            {
                _festivalInfoVM = new FestivalInfoVM(Main, ServiceLocator.Current.GetInstance<IDataService>(), _festivalManagementVM);
                return _festivalInfoVM;
            }
        }

        public AddFestivalVM addFestival
        {
            get
            {
                return new AddFestivalVM(_clientInfoVM);
            }
        }

        public EditFestivalVM editFestival
        {
            get
            {
                return new EditFestivalVM(Main, ServiceLocator.Current.GetInstance<IDataService>(), _clientInfoVM);
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
                _clientInfoVM = new ClientInfoVM(Main, ServiceLocator.Current.GetInstance<IDataService>(), _clientManageVM);
                return _clientInfoVM;
            }
        }

        public EditClientVM editClient
        {
            get
            {
                return new EditClientVM(Main, ServiceLocator.Current.GetInstance<IDataService>(), _clientManageVM);
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