﻿using Festispec.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel.Inspections
{
    public class InspectorAtInspectionVM
    {
        public Inspectors_at_inspection Inspectors_at_inspection { get; set; }

        public InspectorAtInspectionVM(Inspectors_at_inspection inspectors_at_inspection)
        {
            Inspectors_at_inspection = inspectors_at_inspection;
        }
    }
}
