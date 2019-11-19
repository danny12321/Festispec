using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Festispec.Domain;

namespace Festispec.ViewModel.Inspections
{
    public class InspectorsVM
    {

        public Inspectors Inspectors { get; set; }
        public InspectorsVM(Inspectors inspectors)
        {
            Inspectors = inspectors;
        }
    }
}
