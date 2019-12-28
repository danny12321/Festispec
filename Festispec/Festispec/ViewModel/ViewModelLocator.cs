using CommonServiceLocator;
using Festispec.ViewModel.Inspections;
using Festispec.ViewModel.ClientVM;
using Festispec.ViewModel.DataService;
using Festispec.ViewModel.Festival_VMs;
using Festispec.ViewModel.FestivalVM;
using Festispec.ViewModel.FestivalVMs;
using Festispec.ViewModel.Questionnaires;
using Festispec.ViewModel.MunicipalityVM;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using System;
using Festispec.Domain;
using Festispec.ViewModel.Templates;
using Festispec.ViewModel.ContactPersonsVM;
using Festispec.ViewModel.DashboardVM;
using Festispec.ViewModel.InspectorsVM;
using Festispec.ViewModel.Users;

namespace Festispec.ViewModel
{

    public class ViewModelLocator
    {
        private ClientManageVM _clientManageVM;
        private ClientInfoVM _clientInfoVM;
        private FestivalManagementVM _festivalManagementVM;
        private FestivalInfoVM _festivalInfoVM;

        private InspectorsVM.InspectorListViewModel _inspectors;
        private MunicipalityVM.MunicipalityViewModel _municipality;
        
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<IDataService, DataService.DataService>();

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<LoginViewModel>();

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
                return new LoginViewModel(ServiceLocator.Current.GetInstance<IDataService>());
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
                return new FestivalManagementVM(Main, ServiceLocator.Current.GetInstance<IDataService>()); ;
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

        public ContactPersonManageVM contactManagement
        {
            get
            {
                return new ContactPersonManageVM(Main, ServiceLocator.Current.GetInstance<IDataService>());
            }
        }

        public AddContactPersonVM addContactPerson
        {
            get
            {
                return new AddContactPersonVM(contactManagement, ServiceLocator.Current.GetInstance<IDataService>());
            }
        }

        public ContactPersonInfoVM contactPersonInfo
        {
            get
            {
                return new ContactPersonInfoVM(ServiceLocator.Current.GetInstance<IDataService>());
            }
        }

        public AddFestivalContactVM festcalContact
        {
            get
            {
                return new AddFestivalContactVM(Main, ServiceLocator.Current.GetInstance<IDataService>());
            }
        }

        public EditContactPersonVM editContactPerson
        {
            get
            {
                return new EditContactPersonVM(ServiceLocator.Current.GetInstance<IDataService>(), contactManagement);
            }
        }

        public QuestionnairesViewModel Questionnaires
        {
            get
            {
                return new QuestionnairesViewModel(ServiceLocator.Current.GetInstance<IDataService>());
            }
        }

        public InspectorsVM.InspectorListViewModel InspectorsVM
        {
            get
            {
                _inspectors = new InspectorsVM.InspectorListViewModel(Main, ServiceLocator.Current.GetInstance<IDataService>());
                return _inspectors;
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

        public ClientManageVM client
        {
            get
            {
                if (_clientManageVM == null)
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

                return new ViewModel.InspectorsVM.EditInspectorViewModel(_inspectors, ServiceLocator.Current.GetInstance<IDataService>(), Main);
            }
        }

        public InspectorInfoViewModel inspectorinfo
        {
            get
            {
                return new InspectorInfoViewModel(ServiceLocator.Current.GetInstance<IDataService>());
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

        public InspectionsViewModel Inspections
        {
            get
            {
                return new InspectionsViewModel(Main, ServiceLocator.Current.GetInstance<IDataService>());
            }
        }

        public InspectionAddViewModel InspectionAdd
        {
            get
            {
                return new InspectionAddViewModel(Main, ServiceLocator.Current.GetInstance<IDataService>());
            }
        }
        public InspectionEditViewModel InspectionEdit
        {
            get
            {
                return new InspectionEditViewModel(Main, ServiceLocator.Current.GetInstance<IDataService>());
            }
        }

        public TemplatesVM Templates
        {
            get
            {
                return new TemplatesVM(Main, ServiceLocator.Current.GetInstance<IDataService>());
              }
        }

        public ScheduleVM.ScheduleVM Schedule
        {
            get
            {
                return new ScheduleVM.ScheduleVM();
            }
        }

        public ManageDashboardVM Dashboard
        {
            get
            {
                return new ManageDashboardVM();
            }
        }
        public UsersVM UsersViewModel
        {
            get
            {
                return new UsersVM(Main, ServiceLocator.Current.GetInstance<IDataService>());
            }
        }
        public AddUserVM AddUserVM
        {
            get
            {
                return new AddUserVM(Main);
            }
        }

        public EditUserVM EditUserVM
        {
            get
            {
                return new EditUserVM(Main, ServiceLocator.Current.GetInstance<IDataService>());
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}
