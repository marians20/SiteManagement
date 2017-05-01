using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteManagement.Models
{
    public class MeasureUnit
    {
        public MeasureUnit()
        {
            Lvs = new List<Lv>();
        }
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Lv> Lvs { get; set; }
    }
}
