using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel.Inspections
{
    public class InspectionVM
    {
        private Festispec.Domain.Inspections _inspection;

        public int Id
        {
            get { return _inspection.id; }
            set { _inspection.id = value; }
        }
        public string Description
        {
            get { return _inspection.description; }
            set { _inspection.description = value; }
        }
        public DateTime Start_date {
            get { return _inspection.start_date; }
            set { _inspection.start_date = value; }
        }
        public DateTime End_date {
            get { return _inspection.end_date; }
            set { _inspection.end_date = value; }
        }
        public DateTime? Finished {
            get { return _inspection.finished; }
            set { _inspection.finished = value; }
        }
        public int Festival_id {
            get { return _inspection.festival_id; } 
            set { _inspection.festival_id = value; }
        }

        public InspectionVM(Festispec.Domain.Inspections inspection)
        {
            _inspection = inspection;
        }

        public InspectionVM()
        {
            _inspection = new Festispec.Domain.Inspections();
        }

        public Festispec.Domain.Inspections ToModel()
        {
            return _inspection;
        }
    }
}
