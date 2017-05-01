using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SiteManagement.Helpers;
using Sakura.AspNetCore;
using SiteManagement.Data;
using SiteManagement.Import;

namespace SiteManagement.Controllers
{
    public class HomeController : MyBaseController
    {

        public HomeController(ApplicationDbContext context)
            :base(context)
        { }

        public IActionResult Index(int? page, int? pageSize)
        {
            //var importer = new ExcelDataImport(_context)
            //{
            //    InputFileName = @"W:\projects\internal\4Andy\Docs\Proiect vechi\LV.xlsx",
            //};
            //var result = importer.Import();
            var items = _context.Groups;

            return View(items.ToPagedList(pageSize ?? 10, page ?? 1));
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
