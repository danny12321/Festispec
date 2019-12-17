using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FestispecWeb.Models
{
    public class InspectorAvailability
    {
        public int id { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public int inspector_id { get; set; }
    }
}