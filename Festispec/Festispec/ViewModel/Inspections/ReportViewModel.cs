using Festispec.Domain;
using Festispec.ViewModel.DataService;
using Festispec.ViewModel.Questionnaires;
using Festispec.ViewModel.Questionnaires.Types;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using iText.IO.Image;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using Newtonsoft.Json.Linq;
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
        public string Advice { get; set; }
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
            PdfGen(FilePathTemp);


            ToPDF = new RelayCommand(GeneratePdf);

        }
        private void PdfGen(string path)
        {

            using (var writer = new PdfWriter(path))
            {
                using (var pdf = new PdfDocument(writer))
                {

                    var doc = new Document(pdf);
                    CoverSheet(doc);

                    bool isFirstQuestionnaire = true;

                    foreach (QuestionnairesViewModel ql in _questionnaires)
                    {
                        if (!isFirstQuestionnaire)
                        {
                            doc.Add(new AreaBreak());
                        }
                        else
                        {
                            isFirstQuestionnaire = false;
                        }

                        doc.Add(new Paragraph(ql.Questionnaire.name).SetBold().SetFontSize(20));

                        foreach (QuestionViewModel q in ql.Questions)
                        {
                            doc.Add(new Paragraph(q.Question).SetBold().SetFontSize(18));
                            List<IGrouping<DateTime, Answers>> answers;

                            using (var context = new FestispecEntities())
                            {
                                answers = context.Answers.ToList().Where(s => (s.question_id == q.Id)).GroupBy(a => a.insertdate).ToList();
                                context.SaveChanges();
                            }

                            if (answers.Count == 0) continue;

                            switch (q.Type.Id)
                            {
                                case 5: // table
                                    HandleChart(doc, answers.LastOrDefault()?.ToList());
                                    break;

                                case 1: // open

                                    foreach (Answers a in answers.LastOrDefault()?.ToList())
                                    {
                                        doc.Add(new Paragraph(a.answer));
                                    }
                                    break;
                                case 2: // multiple choice

                                    foreach (Answers a in answers.LastOrDefault()?.ToList())
                                    {
                                        doc.Add(new Paragraph($"• {a.answer}"));
                                    }
                                    break;

                                case 3: // select

                                    foreach (Answers a in answers.LastOrDefault()?.ToList())
                                    {
                                        doc.Add(new Paragraph($"• {a.answer}"));
                                    }
                                    break;

                                case 4: // afbeelding
                                    answers?.ForEach(group =>
                                    {
                                        group?.ToList().ForEach(a =>
                                        {
                                            ImageData data1 = ImageDataFactory.Create(Base64ToImage(a.answer));
                                            Image img1 = new Image(data1);
                                            doc.Add(img1);
                                        });
                                    });
                                    break;
                                default:
                                    break;
                            }
                            if (Advice != null)
                            {
                                doc.Add(new Paragraph(Advice));
                            }
                            doc.Add(new Div().SetMarginBottom(30));
                        }
                    }
                }
            }
        }

        private void CoverSheet(Document doc)
        {
            var festival = _service.SelectedFestival;
            var inspection = _service.SelectedInspection;

            var title = new Paragraph("Festispec rapportage - " + festival.FestivalName);
            title.SetFontSize(24).SetBold();
            doc.Add(title);

            // Klant gegevens
            doc.Add(new Paragraph("Klant gegevens").SetFontSize(18).SetBold());
            var clientDetails = new Paragraph($"{festival.FestivalName} - {festival.Street} {festival.HouseNumber}, {festival.City} {festival.PostalCode}");
            doc.Add(clientDetails);

            doc.Add(new Div().SetMarginBottom(20));

            // Inspectie gegevens
            doc.Add(new Paragraph("Inspectie gegevens").SetFontSize(18).SetBold());
            doc.Add(new Paragraph($"{inspection.Description}"));
            doc.Add(new Paragraph($"Van {inspection.Start_date}"));
            doc.Add(new Paragraph($"Tot {inspection.End_date}"));
            doc.Add(new Paragraph($"Inspectie code {inspection.Id}"));

            doc.Add(new Div().SetMarginBottom(20));

            doc.Add(new Paragraph("Creatiedatum").SetFontSize(18).SetBold());
            doc.Add(new Paragraph($"{DateTime.Now}"));

            doc.Add(new AreaBreak());
        }

        private void HandleChart(Document doc, List<Answers> answers)
        {
            var random = new Random();

            if (answers == null || answers.Count <= 0)
            {
                doc.Add(new Paragraph("Geen data ingevuld"));
                return;
            }

            var json = JObject.Parse(answers.Last().answer);
            var xname = json["head"][0].ToString();
            List<List<int>> y = new List<List<int>>();
            var x = new List<string>();

            var head = json["head"].ToList();
            var body = json["body"].ToList();

            // Head -1 for the x as 
            for (int i = 0; i < head.Count - 1; i++)
            {
                y.Add(new List<int>());
            }

            for (int i = 0; i < body.Count; i++)
            { // loop every row
                var row = body[i].ToList();

                for (int j = 0; j < row.Count; j++)
                { // loop every column in the row

                    if (j == 0)
                    {
                        x.Add(row[j].ToString());
                    }
                    else
                    {
                        int value = Int32.Parse(row[j].ToString());
                        y[j - 1].Add(value);
                    }
                }
            }

            var exportFolder1 = Environment.GetFolderPath(Environment.SpecialFolder.InternetCache);
            var exportFile1 = System.IO.Path.Combine(exportFolder1, "TempChartImage" + random.Next() + ".jpg");

            //// create the chart
            var chart = new Chart();
            var legend = new Legend("Legenda");

            var chartArea = new ChartArea("ChartArea");

            chartArea.AxisX.Title = xname;
            chartArea.AxisY.Title = "Waarde";
            chart.ChartAreas.Add(chartArea);

            for (int i = 0; i < y.Count; i++)
            {
                var row = y[i];
                var series = new Series();
                series.ChartType = SeriesChartType.FastLine;
                series.Points.DataBindXY(x, row);
                series.Name = json["head"][i + 1].ToString();
                series.Label = json["head"][i + 1].ToString();

                series.Legend = "Legenda";
                chart.Series.Add(series);

            }

            legend.DockedToChartArea = "ChartArea";
            chart.Legends.Add(legend);

            chart.Width = 600;

            chart.SaveImage(exportFile1, ChartImageFormat.Jpeg);
            ImageData data = ImageDataFactory.Create(exportFile1);
            Image img = new Image(data);
            doc.Add(img);


            //Table

            var table = new Table(head.Count);
            table.UseAllAvailableWidth();

            for (int i = 0; i < head.Count; i++)
            {
                //table.AddCell(head[i].ToString());
                table.AddCell(new Paragraph(head[i].ToString()).SetBold());
            }

            body.ForEach(row =>
            {
                row.ToList().ForEach(column =>
                {
                    table.AddCell(column.ToString());
                });
            });


            doc.Add(table);
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

        private void GeneratePdf()
        {
            PdfGen(FilePath);
        }
    }

}