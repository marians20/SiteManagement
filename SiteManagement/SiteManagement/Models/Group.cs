using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteManagement.Models
{
    public class Group
    {
        public Group()
        {
            Lvs = new List<Lv>();
        }
        public int Id { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }

        public List<Lv> Lvs { get; set; }

        public string FullName => Number.ToString() + " " + Name;
    }
}
