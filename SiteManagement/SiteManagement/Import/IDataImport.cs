using SiteManagement.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteManagement.Import
{
    public interface IDataImport
    {
        StatusInfo Import();
    }
}
