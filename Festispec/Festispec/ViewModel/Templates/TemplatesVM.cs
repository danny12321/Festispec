using Festispec.Domain;
using Festispec.ViewModel.Questionnaires;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Festispec.ViewModel.Templates
{
    public class TemplatesVM
    {
        public ObservableCollection<Festispec.Domain.Questionnaires> Templates { get; set; }

        public ICommand OpenTemplateCommand { get; set; }

        public ICommand AddTemplateCommand { get; set; }

        private MainViewModel _main;

        private DataService.IDataService _service;

        public TemplatesVM(MainViewModel main, DataService.IDataService service)
        {
            _main = main;
            _service = service;

            OpenTemplateCommand = new RelayCommand<Domain.Questionnaires>(OpenTemplate);
            AddTemplateCommand = new RelayCommand(AddTemplate);

            using (var context = new FestispecEntities())
            {
                var templates = context.Questionnaires.Where(q => q.inspection_id == null).ToList();
                Templates = new ObservableCollection<Domain.Questionnaires>(templates);
            }
        }

        private void OpenTemplate(Domain.Questionnaires questionnaire)
        {
            using (var context = new FestispecEntities())
            {
                _service.SelectedQuestionnaire = new QuestionnairesViewModel(context.Questionnaires.FirstOrDefault(q => q.id == questionnaire.id));
                _main.SetPage("Vragenlijsten");
            }
        }

        private void AddTemplate()
        {
            var questionnaire = new Domain.Questionnaires() { name = "Nieuw sjabloon" };

            using (var context = new FestispecEntities())
            {
                context.Questionnaires.Add(questionnaire);
                context.SaveChanges();
            }

            _service.SelectedQuestionnaire = new Questionnaires.QuestionnairesViewModel(questionnaire);
            _main.SetPage("Vragenlijsten");
        }

    }
}
