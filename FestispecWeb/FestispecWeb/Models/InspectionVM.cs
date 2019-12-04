using Festipec.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace FestispecWeb.Models
{
    public class InspectionVM
    {
        public Inspections Inspections { get; set; }
        public IEnumerable<Questionnaires> Questionnaires { get; set; }


        public InspectionVM()
        {
            Inspections = new Inspections();
            Questionnaires = new ObservableCollection<Questionnaires>();
        }

    }
}