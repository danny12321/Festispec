using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FestispecWeb.Models
{
    public class InspectionVM
    {

        public int Id { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime Finished { get; set; }

        public int FestivalId { get; set; }
    }
}