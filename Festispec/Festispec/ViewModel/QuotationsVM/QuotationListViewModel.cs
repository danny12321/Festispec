using Festispec.Domain;
using Festispec.ViewModel.DataService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel.QuotationsVM
{
    public class QuotationListViewModel
    {
        public ObservableCollection<QuotationViewModel> Quotations { get; set; }
        private IDataService _service;
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
        public QuotationListViewModel(IDataService service)
            
        {
            _service = service;
            using (var context = new FestispecEntities())
            {

                var quotations = context.Quotations.Where(q => q.client_id == _service.SelectedFestival.FestivalId).ToList();

                context.SaveChanges();
                Quotations = new ObservableCollection<QuotationViewModel>(quotations.Select(i => new QuotationViewModel(i)));


            }
        }
    }
}
