using Festispec.Domain;
using Festispec.ViewModel.DataService;
using Festispec.ViewModel.Questionnaires;
using Festispec.ViewModel.Questionnaires.Types;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using iText.IO.Image;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
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
        public string FilePathTemp { get; set; }
        public ICommand ToPDF { get; set; }
        private QuestionnairesViewModel _qvm;
        public int Inspection_ID { get; set; }
        private MainViewModel _main;
        private IDataService _service;
        private ObservableCollection<QuestionnairesViewModel> _questionnaires;
        public ReportViewModel(MainViewModel main, IDataService dataService)
        {

            _main = main;
            _service = dataService;

            using (var context = new FestispecEntities())
            {

                var questionlists = context.Questionnaires.Include("Questions").ToList().Where(s => (s.inspection_id == _service.SelectedInspection.Id)).ToList();
               
                _questionnaires = new ObservableCollection<QuestionnairesViewModel>(questionlists.Select(q => new QuestionnairesViewModel(q)));
                context.SaveChanges();
            }
            var exportFolder = Environment.GetFolderPath(Environment.SpecialFolder.InternetCache);
            var exportFile = System.IO.Path.Combine(exportFolder, "Rapportage-" + _service.SelectedFestival.FestivalName + ".pdf");
            var exportFolder2 = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var exportFile2 = System.IO.Path.Combine(exportFolder2, "Rapportage-" + _service.SelectedFestival.FestivalName + ".pdf");
            FilePath = exportFile2;
            FilePathTemp = exportFile;
            GeneratePdfTemp();



            ToPDF = new RelayCommand(GeneratePdf);

        }
        private void PdfGen(string path)
        {
            var random = new Random();
            var random1 = random.Next(2842);
            
            var exportFolder1 = Environment.GetFolderPath(Environment.SpecialFolder.InternetCache);
            var exportFile1 = System.IO.Path.Combine(exportFolder1, "TempChartImage"+ random1  +".jpg");

            using (var writer = new PdfWriter(path))
            {
                using (var pdf = new PdfDocument(writer))
                {

                    var doc = new Document(pdf);
                    doc.Add(new Paragraph("Rapportage - " + _service.SelectedFestival.FestivalName));
                    foreach (QuestionnairesViewModel ql in _questionnaires)
                    {

                        foreach (QuestionViewModel q in ql.Questions)
                        {
                            doc.Add(new Paragraph(q.Question + ": "));
                            var answers = new List<Answers>();
                            using (var context = new FestispecEntities())
                            {

                               answers = context.Answers.ToList().Where(s => (s.question_id == q.Id)).ToList();

                                context.SaveChanges();
                            }

                            switch (q.Type.Type.ToLower())
                            {
                                case "table":
                                    var y = new List<int>();
                                    var x = new List<string>();
                                    int index = 0;
                                    foreach (Answers a in answers)
                                    {
                                        if (index > ((answers.Count / 2) - 1))
                                        {
                                            y.Add(Int32.Parse(a.answer));
                                        }
                                        else
                                        {
                                            x.Add(a.answer);
                                            index++;
                                        }
                                    }
                      

                                    // create the chart
                                    var chart = new Chart();


                                    var chartArea = new ChartArea();
                                  //  chartArea.AxisX.LabelStyle.Format = "\nhh:mm";

                                    chart.ChartAreas.Add(chartArea);

                                    var series = new Series();
                                    series.Name = "Series1";
                                    series.ChartType = SeriesChartType.FastLine;
                                 //   series.XValueType = ChartValueType.String;
                                    chart.Series.Add(series);

                                    // bind the datapoints
                                    chart.Series["Series1"].Points.DataBindXY(x, y);

                                    chart.SaveImage(exportFile1, ChartImageFormat.Jpeg);
                                    ImageData data = ImageDataFactory.Create(exportFile1);
                                    Image img = new Image(data);
                                    doc.Add(img);
                                    break;

                                case "open":

                                    foreach (Answers a in answers)
                                    {
                                        doc.Add(new Paragraph(a.answer));
                                    }
                                    break;
                                case "multiple choise":

                                    foreach (Answers a in answers)
                                    {
                                        doc.Add(new Paragraph(a.answer));
                                    }
                                    break;
                                case "select":

                                    foreach (Answers a in answers)
                                    {
                                        doc.Add(new Paragraph(a.answer));
                                    }
                                    break;
                                default:

                                    break;



                            }

                        }

                    }


                }
            }
        }
        private void GeneratePdfTemp()
        {


            // copy the series and manipulate the copy
            //   chart.DataManipulator.CopySeriesValues("Series1", "Series2");
            // chart.DataManipulator.FinancialFormula(
            //    FinancialFormula.WeightedMovingAverage,
            //  "Series2"
            // );
            // chart.Series["Series2"].ChartType = SeriesChartType.FastLine;





            PdfGen(FilePathTemp);
        }
        private void GeneratePdf()
        {
            PdfGen(FilePath);
        }
    }

}