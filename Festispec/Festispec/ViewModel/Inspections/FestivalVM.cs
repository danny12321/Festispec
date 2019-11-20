﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel.Inspections
{
    public class FestivalVM
    {

        public Festispec.Domain.Festivals Festivals { get; set; }

        public string Pos { get { return Festivals.latitude + "," + Festivals.longitude; } }
        public FestivalVM(Festispec.Domain.Festivals festival)
        {
            Festivals = festival;
        }
    }
}
