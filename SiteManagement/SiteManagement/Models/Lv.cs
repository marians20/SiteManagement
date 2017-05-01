using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace SiteManagement.Models
{
    public class Lv
    {
        public int Id { get; set; }
        public string PosNr { get; set; }
        [DisplayName("Group")]
        public int GroupId { get; set; }
        public Group Group {get; set;}
        public string Name { get; set; }
        public decimal Ammount { get; set; }
        [DisplayName("Measure Unit")]
        public int MeasureUnitId { get; set; }
        public MeasureUnit MeasureUnit { get; set; }
        public decimal Ep { get; set; }
        public decimal Gp { get; set; }
    }
}
