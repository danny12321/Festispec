using Festispec.Domain;
using Festispec.ViewModel.DataService;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Festispec.ViewModel.QuotationsVM
{
    public class AddQuotationViewModel
    {
        private IDataService _service;
        public ICommand AddNew { get; set; }

        private MainViewModel _mainViewModel;
        public QuotationViewModel Quotation { get; set; }
        public AddQuotationViewModel(IDataService dataService, MainViewModel main)
        {

            _service = dataService;
            _mainViewModel = main;
            Quotation = new QuotationViewModel();
            Quotation.Price = 0;
            AddNew = new RelayCommand(Add);
        }
        private void Add()
        {
            Quotation.FestivalId = _service.SelectedFestival.FestivalId;
            Quotation.Approved = DateTime.Now;
            using (var context = new FestispecEntities())
            {

                context.Quotations.Add(Quotation.ToModel());
                context.SaveChanges();
                Quotation.Approved = null;
                context.Quotations.Attach(Quotation.ToModel());
                context.SaveChanges();

            }
            _mainViewModel.SetPage("ShowQuotations");
        }
    }
}
