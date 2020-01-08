﻿using Festispec.Domain;
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
using System.Windows.Media.Imaging;
//using System.Windows.Media.Imaging;

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

                            switch (q.Type.Id)
                            {
                                case 8: // table
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

                                case 1: // open

                                    foreach (Answers a in answers)
                                    {
                                        doc.Add(new Paragraph(a.answer));
                                    }
                                    break;
                                case 2: // multiple choice

                                    foreach (Answers a in answers)
                                    {
                                        doc.Add(new Paragraph(a.answer));
                                    }
                                    break;

                                case 3: // select

                                    foreach (Answers a in answers)
                                    {
                                        doc.Add(new Paragraph(a.answer));
                                    }
                                    break;
                                case 4: // afbeelding

                                    foreach (Answers a in answers)
                                    {


                                        ImageData data1 = ImageDataFactory.Create(Base64ToImage(a.answer));
                                        Image img1 = new Image(data1);
                                        doc.Add(img1);
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
        public String Base64ToImage(string base64String)
        {
            byte[] imageBytes = Convert.FromBase64String(base64String);
            BitmapImage bitmapImage = new BitmapImage();
            MemoryStream ms = new MemoryStream(imageBytes);
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = ms;
            bitmapImage.EndInit();
           
            BitmapEncoder encoder = new PngBitmapEncoder();
            var random = new Random();
            encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
            var exportFolder = Environment.GetFolderPath(Environment.SpecialFolder.InternetCache);
            var exportFile = System.IO.Path.Combine(exportFolder, "plaatje-" + random.Next(2000) + ".jpg");
            using (var fileStream = new System.IO.FileStream(exportFile, System.IO.FileMode.Create))
            {
                encoder.Save(fileStream);
            }
            return exportFile;
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