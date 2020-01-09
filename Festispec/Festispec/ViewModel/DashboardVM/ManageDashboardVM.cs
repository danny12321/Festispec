using Festispec.Domain;
using Festispec.ViewModel.InspectorsVM;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveCharts;
using LiveCharts.Wpf;
using Festispec.ViewModel.Inspections;

namespace Festispec.ViewModel.DashboardVM
{
    public class ManageDashboardVM : ViewModelBase
    {
        private int _january;
        private int _february;
        private int _march;
        private int _april;
        private int _may;
        private int _june;
        private int _july;
        private int _august;
        private int _september;
        private int _october;
        private int _november;
        private int _december;
        private double _january2;
        private double _february2;
        private double _march2;
        private double _april2;
        private double _may2;
        private double _june2;
        private double _july2;
        private double _august2;
        private double _september2;
        private double _october2;
        private double _november2;
        private double _december2;

        public string CurrentYear { get; set; }
        public ObservableCollection<InspectorviewModel> Inspectors { get; set; }
        public string RevenueAmount { get; set; }
        public SeriesCollection SeriesCollection { get; set; }
        public SeriesCollection SeriesCollection2 { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }
        public Func<double, string> YFormatter { get; set; }

        public ManageDashboardVM()
        {
            CurrentYear = DateTime.Now.Year.ToString();
            using (var context = new FestispecEntities())
            {
                var inspectors = context.Inspectors.ToList();
                var quotation = context.Quotations.ToList().Select(i => new QuotationsVM.QuotationViewModel(i));
                var inspections = context.Inspections.ToList();

                Inspectors = new ObservableCollection<InspectorviewModel>(inspectors.Select(i => new InspectorviewModel(i)).Where(i => i.Active != null));
                var availablequotations = quotation.Where(q => q.Approved1 != null).ToList();
                var sum = availablequotations.Where(q => q.Approved.Month == DateTime.Now.Month).Select(q => q.Price).Sum();
                var text = (double)sum;
                RevenueAmount = "€" + text.ToString();

                _january = inspections.Where(i => i.start_date.Month == 1 && i.start_date.Year == DateTime.Now.Year).Select(i => i.id).Count();
                _february = inspections.Where(i => i.start_date.Month == 2 && i.start_date.Year == DateTime.Now.Year).Select(i => i.id).Count();
                _march = inspections.Where(i => i.start_date.Month == 3 && i.start_date.Year == DateTime.Now.Year).Select(i => i.id).Count();
                _april = inspections.Where(i => i.start_date.Month == 4 && i.start_date.Year == DateTime.Now.Year).Select(i => i.id).Count();
                _may = inspections.Where(i => i.start_date.Month == 5 && i.start_date.Year == DateTime.Now.Year).Select(i => i.id).Count();
                _june = inspections.Where(i => i.start_date.Month == 6 && i.start_date.Year == DateTime.Now.Year).Select(i => i.id).Count();
                _july = inspections.Where(i => i.start_date.Month == 7 && i.start_date.Year == DateTime.Now.Year).Select(i => i.id).Count();
                _august = inspections.Where(i => i.start_date.Month == 8 && i.start_date.Year == DateTime.Now.Year).Select(i => i.id).Count();
                _september = inspections.Where(i => i.start_date.Month == 9 && i.start_date.Year == DateTime.Now.Year).Select(i => i.id).Count();
                _october = inspections.Where(i => i.start_date.Month == 10 && i.start_date.Year == DateTime.Now.Year).Select(i => i.id).Count();
                _november = inspections.Where(i => i.start_date.Month == 11 && i.start_date.Year == DateTime.Now.Year).Select(i => i.id).Count();
                _december = inspections.Where(i => i.start_date.Month == 12 && i.start_date.Year == DateTime.Now.Year).Select(i => i.id).Count();

                _january2 = (double)availablequotations.Where(q => q.Approved.Month == 1 && q.Approved.Year == DateTime.Now.Year).Select(q => q.Price).Sum();
                _february2 = (double)availablequotations.Where(q => q.Approved.Month == 2 && q.Approved.Year == DateTime.Now.Year).Select(q => q.Price).Sum();
                _march2 = (double)availablequotations.Where(q => q.Approved.Month == 3 && q.Approved.Year == DateTime.Now.Year).Select(q => q.Price).Sum();
                _april2 = (double)availablequotations.Where(q => q.Approved.Month == 4 && q.Approved.Year == DateTime.Now.Year).Select(q => q.Price).Sum();
                _may2 = (double)availablequotations.Where(q => q.Approved.Month == 5 && q.Approved.Year == DateTime.Now.Year).Select(q => q.Price).Sum();
                _june2 = (double)availablequotations.Where(q => q.Approved.Month == 6 && q.Approved.Year == DateTime.Now.Year).Select(q => q.Price).Sum();
                _july2 = (double)availablequotations.Where(q => q.Approved.Month == 7 && q.Approved.Year == DateTime.Now.Year).Select(q => q.Price).Sum();
                _august2 = (double)availablequotations.Where(q => q.Approved.Month == 8 && q.Approved.Year == DateTime.Now.Year).Select(q => q.Price).Sum();
                _september2 = (double)availablequotations.Where(q => q.Approved.Month == 9 && q.Approved.Year == DateTime.Now.Year).Select(q => q.Price).Sum();
                _october2 = (double)availablequotations.Where(q => q.Approved.Month == 10 && q.Approved.Year == DateTime.Now.Year).Select(q => q.Price).Sum();
                _november2 = (double)availablequotations.Where(q => q.Approved.Month == 11 && q.Approved.Year == DateTime.Now.Year).Select(q => q.Price).Sum();
                _december2 = (double)availablequotations.Where(q => q.Approved.Month == 12 && q.Approved.Year == DateTime.Now.Year).Select(q => q.Price).Sum();

            }

            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Inspecties",
                    Values = new ChartValues<int> { _january, _february, _march, _april, _may, _june, _july, _august, _september, _october, _november, _december }
                }
            };

            SeriesCollection2 = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Opbrengst",
                    Values = new ChartValues<double> { _january2, _february2, _march2, _april2, _may2, _june2, _july2, _august2, _september2, _october2, _november2, _december2 }
                },
            };

            Labels = new[] { "Jan", "Feb", "Maart", "April", "Mei", "Juni", "Juli", "Aug", "Sept", "Oct", "Nov", "Dec", };
            Formatter = value => value.ToString("N2");
            YFormatter = value => value.ToString("C");

        }
    }
}
