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

        public ObservableCollection<InspectorviewModel> Inspectors { get; set; }
        public string RevenueAmount { get; set; }
        public SeriesCollection SeriesCollection { get; set; }
        public SeriesCollection SeriesCollection2 { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }
        public Func<double, string> YFormatter { get; set; }

        public ManageDashboardVM()
        {
            using (var context = new FestispecEntities())
            {

                var inspectors = context.Inspectors.ToList();
                var quotation = context.Quotations.ToList();
                var inspections = context.Inspections.ToList();

                Inspectors = new ObservableCollection<InspectorviewModel>(inspectors.Select(i => new InspectorviewModel(i)).Where(i => i.Active != null));
                // check voor deze maand en of ze accepted zijn
                var sum = quotation.Select(q => q.price).Sum();
                var text = (double)sum;
                RevenueAmount = "€" + text.ToString();

                // check voor elke maand inspecties en effe kijken wat te doen met jaar
                _january = inspections.Select(i => i.id).Count();
                _february = inspections.Select(i => i.id).Count();
                _march = inspections.Select(i => i.id).Count();
                _april = inspections.Select(i => i.id).Count();
                _may = inspections.Select(i => i.id).Count();
                _june = inspections.Select(i => i.id).Count();
                _july = inspections.Select(i => i.id).Count();
                _august = inspections.Select(i => i.id).Count();
                _september = inspections.Select(i => i.id).Count();
                _october = inspections.Select(i => i.id).Count();
                _november = inspections.Select(i => i.id).Count();
                _december = inspections.Select(i => i.id).Count();

                // check voor elke maand offertes en effe kijken wat te doen met jaar
                _january2 = (double)quotation.Select(q => q.price).Sum();
                _february2 = (double)quotation.Select(q => q.price).Sum();
                _march2 = (double)quotation.Select(q => q.price).Sum();
                _april2 = (double)quotation.Select(q => q.price).Sum();
                _may2 = (double)quotation.Select(q => q.price).Sum();
                _june2 = (double)quotation.Select(q => q.price).Sum();
                _july2 = (double)quotation.Select(q => q.price).Sum();
                _august2 = (double)quotation.Select(q => q.price).Sum();
                _september2 = (double)quotation.Select(q => q.price).Sum();
                _october2 = (double)quotation.Select(q => q.price).Sum();
                _november2 = (double)quotation.Select(q => q.price).Sum();
                _december2 = (double)quotation.Select(q => q.price).Sum();

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
                    Title = "Series 1",
                    Values = new ChartValues<double> { _january2, _february2, _march2, _april2, _may2, _june2, _july2, _august2, _september2, _october2, _november2, _december2 }
                },
            };

            Labels = new[] { "Januari", "Februari", "Maart", "April", "Mei", "Juni", "Juli", "Augustus", "September", "October", "November", "December", };
            Formatter = value => value.ToString("N");
            YFormatter = value => value.ToString("C");

        }
    }
}
