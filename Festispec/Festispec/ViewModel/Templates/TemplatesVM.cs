using Festispec.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel.Templates
{
    public class TemplatesVM
    {

        public ObservableCollection<Festispec.Domain.Questionnaires> Templates;

        public TemplatesVM()
        {

            using (var context = new FestispecEntities())   
            {
                var templates = context.Questionnaires.Where(q => q.inspection_id == null);
                
            }
        }
    }
}
