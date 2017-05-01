using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteManagement.Helpers
{
    public class ExcelDTO
    {
        [ExcelField(fieldName: "Pos.-Nr.")]
        public string PosNr { get; set; }
        [ExcelField(fieldName: "Group")]
        public string Group { get; set; }
        [ExcelField(fieldName: "Kurz- und Langtext")]
        public string KurzUndLangtext { get; set; }
        [ExcelField(fieldName: "Menge")]
        public string Menge { get; set; }
        [ExcelField(fieldName: "Einh.")]
        public string Enih { get; set; }
        [ExcelField(fieldName: "L-EP-iBlue")]
        public string LEPiBlue { get; set; }
        [ExcelField(fieldName: "L-GP-iBlue")]
        public string LGPiBlue { get; set; }
    }
}
