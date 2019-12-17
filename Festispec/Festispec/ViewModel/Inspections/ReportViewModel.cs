using Festispec.Domain;
using Festispec.ViewModel.Questionnaires;
using Festispec.ViewModel.Questionnaires.Types;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using iText.IO.Image;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using PdfPrintingNet;
using PdfViewerNet;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.DataVisualization.Charting;
using System.Windows;
using System.Windows.Input;

namespace Festispec.ViewModel.Inspections
{
    public class ReportViewModel : ViewModelBase
    {
        public string FilePath { get; set; }
        public ICommand ToPDF { get; set; }
        private QuestionnairesViewModel _qvm;
        public int Inspection_ID { get; set; }
        public ReportViewModel()
        {
            var exportFolder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var exportFile = System.IO.Path.Combine(exportFolder, "Rapportage-FestivalNaam.pdf");
            var exportFolder1 = Environment.GetFolderPath(Environment.SpecialFolder.InternetCache);
            var exportFile1 = System.IO.Path.Combine(exportFolder1, "TempChartImage.jpg");
            GeneratePdf();
            FilePath = exportFile;

            ToPDF = new RelayCommand(GeneratePdf);

        }

        private void GeneratePdf()
        {
            var xvals = new[]
            {
                "13:20",
                "13:22",
                "13:24",
                "13:26",
                "13:28",
                "13:30",
                "13:32"
            };
            var yvals = new[] { 100, 300, 150, 200, 100,150  ,200  };

            // create the chart
            var chart = new Chart();
          

            var chartArea = new ChartArea();
            chartArea.AxisX.LabelStyle.Format = "\nhh:mm";
      
            chart.ChartAreas.Add(chartArea);

            var series = new Series();
            series.Name = "Series1";
            series.ChartType = SeriesChartType.FastLine;
            series.XValueType = ChartValueType.Time;
            chart.Series.Add(series);

            // bind the datapoints
            chart.Series["Series1"].Points.DataBindXY(xvals, yvals);

            // copy the series and manipulate the copy
         //   chart.DataManipulator.CopySeriesValues("Series1", "Series2");
           // chart.DataManipulator.FinancialFormula(
            //    FinancialFormula.WeightedMovingAverage,
              //  "Series2"
           // );
           // chart.Series["Series2"].ChartType = SeriesChartType.FastLine;

            
            

            var exportFolder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var exportFile = System.IO.Path.Combine(exportFolder, "Rapportage-FestivalNaam.pdf");
            var exportFolder1 = Environment.GetFolderPath(Environment.SpecialFolder.InternetCache);
            var exportFile1 = System.IO.Path.Combine(exportFolder1, "TempChartImage.jpg");
            FilePath = exportFile;
            using (var writer = new PdfWriter(exportFile))
            {
                using (var pdf = new PdfDocument(writer))
                {
                    
                    chart.SaveImage(exportFile1, ChartImageFormat.Jpeg);
                    ImageData data = ImageDataFactory.Create(exportFile1);
                    Image img = new Image(data);
                    var doc = new Document(pdf);
                    doc.Add(new Paragraph("Report - defqon"));
                    doc.Add(img);
                }
            }
        }
    }
}
