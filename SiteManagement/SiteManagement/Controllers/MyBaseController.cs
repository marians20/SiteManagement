using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SiteManagement.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SiteManagement.Controllers
{
    public class MyBaseController : Controller
    {
        protected readonly ApplicationDbContext _context;

        public MyBaseController(ApplicationDbContext context)
        {
            _context = context;

        }

        protected void AddGroupsSelectList(object selectedValue = null)
        {
            ViewData["GroupId"] = new SelectList(_context.Groups.OrderBy(x => x.Number),
                "Id", "FullName", selectedValue);
        }

        protected void AddMeasureUnitsSelectList(object selectedValue = null)
        {
            ViewData["MeasureUnitId"] = new SelectList(_context.MeasureUnits.OrderBy(x => x.Name.ToLower()),
                "Id", "Name", selectedValue);
        }
    }
}