using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel.QuotationsVM
{
    public class AddQuotationViewModel
    {
        public QuotationViewModel Quotation { get; set; }
        public AddQuotationViewModel()
        {
            Quotation = new QuotationViewModel();
        }
    }
}
