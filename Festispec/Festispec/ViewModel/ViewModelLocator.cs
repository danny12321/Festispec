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
using Festispec.ViewModel.ContactPersonsVM;

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

            SimpleIoc.Default.Register<FestispecEntities>();
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<LoginViewModel>();
            SimpleIoc.Default.Register<FestivalManagementVM>();
            SimpleIoc.Default.Register<QuestionnairesViewModel>();
           

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
                return ServiceLocator.Current.GetInstance<LoginViewModel>();
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
                return ServiceLocator.Current.GetInstance<FestivalManagementVM>();
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
                return ServiceLocator.Current.GetInstance<QuestionnairesViewModel>();
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

        public ReportViewModel ReportVM
        {
            get
            {
                return new ReportViewModel();
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}
