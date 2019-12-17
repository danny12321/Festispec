using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Festispec.Domain;

namespace Festispec.ViewModel.QuotationsVM
{
    public class QuotationViewModel
    {
        private Quotations _q;
        public QuotationViewModel()
        {
            _q = new Quotations();
        }
        public QuotationViewModel(Quotations q)
        {
            _q = q;
        }
        public int QuotationId
        {
            get { return _q.id; }
            set { _q.id = value; }
        }
        public double Price
        {
            get { return (double)_q.price; }
            set { _q.price = (decimal)value; }
        }
        public int FestivalId
        {
            get { return (int)_q.festival_id; }
            set { _q.festival_id = value; }
        }

        public DateTime? Approved
        {
            get
            {
                return _q.approved;
            }
            set
            {
                _q.approved = value;
            }
        }

        public string Qname
        {
            get
            {
                return " Offerte " + QuotationId;
            }
        }
        internal Quotations ToModel()
        {
            return _q;
        }
    }
}
