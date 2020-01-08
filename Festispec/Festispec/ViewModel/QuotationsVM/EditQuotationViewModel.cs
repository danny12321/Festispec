using Festispec.Domain;
using Festispec.ViewModel.DataService;
using GalaSoft.MvvmLight.Command;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Festispec.ViewModel.QuotationsVM
{
    public class EditQuotationViewModel
    {
        public String FilePath { get; set; }

        private IDataService _service;
        public ICommand Save { get; set; }
        public ICommand ToPdf { get; set; }
        public bool Approved { get; set; }
        private MainViewModel _mainViewModel;
        public QuotationViewModel SelectedQuotation
        {
            get
            {
                return _service.SelectedQuotation;
            }
        }
        public EditQuotationViewModel(IDataService dataService, MainViewModel main)
        {

            _service = dataService;
            _mainViewModel = main;
            var quotationsName = "Offerte" + _service.SelectedQuotation.QuotationId + "-" + _service.SelectedFestival.FestivalName + ".pdf";
            var exportFolder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            FilePath = exportFolder;
            var exportFile = System.IO.Path.Combine(exportFolder, quotationsName);
            FilePath = exportFile;
            Approved = _service.SelectedQuotation.Approved != null;
            ToPdf = new RelayCommand(GeneatePDF);
            Save = new RelayCommand(Edit);
        }
        private void GeneatePDF()
        {


            using (var writer = new PdfWriter(FilePath))
            {
                using (var pdf = new PdfDocument(writer))
                {


                    var doc = new Document(pdf);

                    doc.Add(new Paragraph("Offerte - " + _service.SelectedFestival.FestivalName));
                    doc.Add(new Paragraph("De prijs voor de inspectie is: " + SelectedQuotation.Price));

                }
            }
        }
        private void Edit()
        {
            using (var context = new FestispecEntities())
            {
                if (Approved)
                {
                    if(_service.SelectedQuotation.Approved == null)
                    {
                        _service.SelectedQuotation.Approved = DateTime.Now;
                        
                    }
                }
                else
                {
                    _service.SelectedQuotation.Approved = null;
                }
                context.Entry(_service.SelectedQuotation.ToModel()).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            _mainViewModel.SetPage("ShowQuotations");
        }
    }
}
