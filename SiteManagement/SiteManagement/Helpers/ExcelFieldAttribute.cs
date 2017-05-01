using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteManagement.Helpers
{
    public class ExcelFieldAttribute : Attribute
    {
        public string FieldName { get; set; }

        public ExcelFieldAttribute(string fieldName)
        {
            FieldName = fieldName;
        }
    }
}
