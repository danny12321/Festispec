using Festispec.Domain;
using Festispec.ViewModel.DataService;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Festispec.ViewModel.QuotationsVM
{
    public class QuotationListViewModel
    {
        public ObservableCollection<QuotationViewModel> Quotations { get; set; }
        private IDataService _service;
        public ICommand AddQuotation { get; set; }
        public ICommand EditQuotation { get; set; }
        private MainViewModel _mainViewModel;
       

        public QuotationViewModel SelectedQ
        {
            get
            {
                return _service.SelectedQuotation;
            }
            set
            {
                _service.SelectedQuotation = value;
            }
        }
        public QuotationListViewModel(IDataService service, MainViewModel MV)


        {
            AddQuotation = new RelayCommand(Add);
            EditQuotation = new RelayCommand(Edit);
            _mainViewModel = MV;
            _service = service;
            using (var context = new FestispecEntities())
            {

                var quotations = context.Quotations.Where(q => q.festival_id == _service.SelectedFestival.FestivalId).ToList();

                context.SaveChanges();
                Quotations = new ObservableCollection<QuotationViewModel>(quotations.Select(i => new QuotationViewModel(i)));


            }

        }
        private void Add()
        {
            _mainViewModel.SetPage("AddQuotation");

        }
        private void Edit()
        {
            _mainViewModel.SetPage("EditQuotation");

        }
    }
}
