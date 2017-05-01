using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SiteManagement.Data;
using SiteManagement.Models;

namespace SiteManagement.Controllers
{
    public class MeasureUnitsController : MyBaseController
    {
        public MeasureUnitsController(ApplicationDbContext context)
            :base(context)
        { }

        // GET: MeasureUnits
        public async Task<IActionResult> Index()
        {
            return View(await _context.MeasureUnits.ToListAsync());
        }

        // GET: MeasureUnits/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var measureUnit = await _context.MeasureUnits
                .SingleOrDefaultAsync(m => m.Id == id);
            if (measureUnit == null)
            {
                return NotFound();
            }

            return View(measureUnit);
        }

        // GET: MeasureUnits/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MeasureUnits/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] MeasureUnit measureUnit)
        {
            if (ModelState.IsValid)
            {
                _context.Add(measureUnit);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(measureUnit);
        }

        // GET: MeasureUnits/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var measureUnit = await _context.MeasureUnits.SingleOrDefaultAsync(m => m.Id == id);
            if (measureUnit == null)
            {
                return NotFound();
            }
            return View(measureUnit);
        }

        // POST: MeasureUnits/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] MeasureUnit measureUnit)
        {
            if (id != measureUnit.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(measureUnit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MeasureUnitExists(measureUnit.Id))
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
            return View(measureUnit);
        }

        // GET: MeasureUnits/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var measureUnit = await _context.MeasureUnits
                .SingleOrDefaultAsync(m => m.Id == id);
            if (measureUnit == null)
            {
                return NotFound();
            }

            return View(measureUnit);
        }

        // POST: MeasureUnits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var measureUnit = await _context.MeasureUnits.SingleOrDefaultAsync(m => m.Id == id);
            _context.MeasureUnits.Remove(measureUnit);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool MeasureUnitExists(int id)
        {
            return _context.MeasureUnits.Any(e => e.Id == id);
        }
    }
}
