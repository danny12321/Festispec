using Festispec.Domain;
using Festispec.ViewModel.InspectorsVM;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel.DashboardVM
{
    public class ManageDashboardVM : ViewModelBase
    {
        private int _amountOfDays;

        public ObservableCollection<InspectorviewModel> Inspectors { get; set; }
        public string RevenueAmount { get; set; }
        public ManageDashboardVM()
        {
            using (var context = new FestispecEntities())
            {

                var inspectors = context.Inspectors.ToList();
                var quotation = context.Quotations.ToList();

                Inspectors = new ObservableCollection<InspectorviewModel>(inspectors.Select(i => new InspectorviewModel(i)).Where(i => i.Active != null));
                var sum = quotation.Select(q => q.price).Sum();
                var text = (double)sum;
                RevenueAmount = "€" + text.ToString();
            }
        }
    }
}
