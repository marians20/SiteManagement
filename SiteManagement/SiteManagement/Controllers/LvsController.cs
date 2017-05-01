using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SiteManagement.Data;
using SiteManagement.Models;
using Sakura.AspNetCore;

namespace SiteManagement.Controllers
{
    public class LvsController : MyBaseController
    {
        public LvsController(ApplicationDbContext context)
            :base(context)
        { }

        // GET: Lvs
        public IActionResult Index(string searchString, string sortOrder, int? page, int? pageSize)
        {
            IQueryable<Lv> items = _context.Lvs.Include(l => l.Group).Include(l => l.MeasureUnit);

            ViewData["CurrentFilter"] = searchString;

            ViewData["PosNrSortParam"] = sortOrder == "posnr" ? "posnr_desc" : "posnr";
            ViewData["NameSortParam"] = String.IsNullOrEmpty(sortOrder) || sortOrder == "name" ? "name_desc" : "name";
            ViewData["AmmountSortParam"] = sortOrder == "ammount" ? "ammount_desc" : "ammount";
            ViewData["GpSortParam"] = sortOrder == "gp" ? "gp_desc" : "gp";
            ViewData["EpSortParam"] = sortOrder == "ep" ? "ep_desc" : "ep";
            ViewData["GroupSortParam"] = sortOrder == "groupname" ? "groupname_desc" : "groupname";
            ViewData["MeasureUnitSortParam"] = sortOrder == "measureunit" ? "measureunit_desc" : "measureunit";

            if (!String.IsNullOrEmpty(searchString))
            {
                searchString = searchString.Trim().ToLower();
                items = items.Where(s => s.Name.ToLower().Contains(searchString));
            }

            switch (sortOrder)
            {
                case "posnr":
                    items = items.OrderBy(x => x.PosNr);
                    break;
                case "posnr_desc":
                    items = items.OrderByDescending(x => x.PosNr);
                    break;
                case "name":
                    items = items.OrderBy(x => x.Name);
                    break;
                case "name_desc":
                    items = items.OrderByDescending(x => x.Name);
                    break;
                case "ammount":
                    items = items.OrderBy(x => x.Ammount);
                    break;
                case "ammount_desc":
                    items = items.OrderByDescending(x => x.Ammount);
                    break;
                case "gp":
                    items = items.OrderBy(x => x.Gp);
                    break;
                case "gp_desc":
                    items = items.OrderByDescending(x => x.Gp);
                    break;
                case "ep":
                    items = items.OrderBy(x => x.Ep);
                    break;
                case "ep_desc":
                    items = items.OrderByDescending(x => x.Ep);
                    break;
                case "groupname":
                    items = items.OrderBy(x => x.Group.Name);
                    break;
                case "groupname_desc":
                    items = items.OrderByDescending(x => x.Group.Name);
                    break;
                case "measureunit":
                    items = items.OrderBy(x => x.MeasureUnit.Name);
                    break;
                case "measureunit_desc":
                    items = items.OrderByDescending(x => x.MeasureUnit.Name);
                    break;
            }
            AddGroupsSelectList();
            AddMeasureUnitsSelectList();
            return View(items.ToPagedList(pageSize ?? 10, page ?? 1));
        }

        // GET: Lvs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lv = await _context.Lvs
                .Include(l => l.Group)
                .Include(l => l.MeasureUnit)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (lv == null)
            {
                return NotFound();
            }

            return View(lv);
        }

        // GET: Lvs/Create
        public IActionResult Create()
        {
            AddGroupsSelectList();
            AddMeasureUnitsSelectList();
            //ViewData["GroupId"] = new SelectList(_context.Groups, "Id", "Name");
            //ViewData["MeasureUnitId"] = new SelectList(_context.MeasureUnits, "Id", "Name");
            return View();
        }

        // POST: Lvs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PosNr,GroupId,Name,Ammount,MeasureUnitId,Ep,Gp")] Lv lv)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lv);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            //ViewData["GroupId"] = new SelectList(_context.Groups, "Id", "Name", lv.GroupId);
            //ViewData["MeasureUnitId"] = new SelectList(_context.MeasureUnits, "Id", "Name", lv.MeasureUnitId);
            AddGroupsSelectList(lv.GroupId);
            AddMeasureUnitsSelectList(lv.MeasureUnitId);
            return View(lv);
        }

        // GET: Lvs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lv = await _context.Lvs.SingleOrDefaultAsync(m => m.Id == id);
            if (lv == null)
            {
                return NotFound();
            }
            //ViewData["GroupId"] = new SelectList(_context.Groups, "Id", "Name", lv.GroupId);
            //ViewData["MeasureUnitId"] = new SelectList(_context.MeasureUnits, "Id", "Name", lv.MeasureUnitId);
            AddGroupsSelectList(lv.GroupId);
            AddMeasureUnitsSelectList(lv.MeasureUnitId);
            return View(lv);
        }

        // POST: Lvs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PosNr,GroupId,Name,Ammount,MeasureUnitId,Ep,Gp")] Lv lv)
        {
            if (id != lv.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lv);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LvExists(lv.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            //ViewData["GroupId"] = new SelectList(_context.Groups, "Id", "Id", lv.GroupId);
            //ViewData["MeasureUnitId"] = new SelectList(_context.MeasureUnits, "Id", "Id", lv.MeasureUnitId);
            AddGroupsSelectList(lv.GroupId);
            AddMeasureUnitsSelectList(lv.MeasureUnitId);
            return View(lv);
        }

        // GET: Lvs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lv = await _context.Lvs
                .Include(l => l.Group)
                .Include(l => l.MeasureUnit)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (lv == null)
            {
                return NotFound();
            }

            return View(lv);
        }

        // POST: Lvs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lv = await _context.Lvs.SingleOrDefaultAsync(m => m.Id == id);
            _context.Lvs.Remove(lv);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool LvExists(int id)
        {
            return _context.Lvs.Any(e => e.Id == id);
        }
    }
}
