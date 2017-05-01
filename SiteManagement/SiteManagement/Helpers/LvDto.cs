using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteManagement.Helpers
{
    public class LvDto
    {
        [ExcelField(fieldName: "Pos.-Nr.")]
        public string PosNr { get; set; }

        [ExcelField(fieldName: "Gruppe")]
        public string Gruppe { get; set; }

        [ExcelField(fieldName: "Bezeichnung")]
        public string Bezeichnung { get; set; }

        [ExcelField(fieldName: "Menge")]
        public string Menge { get; set; }

        [ExcelField(fieldName: "Einh.")]
        public string Enih { get; set; }

        [ExcelField(fieldName: "EP")]
        public string Ep { get; set; }

        [ExcelField(fieldName: "GP")]
        public string Gp { get; set; }
    }
}
