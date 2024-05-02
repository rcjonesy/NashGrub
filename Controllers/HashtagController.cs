using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NashGrub.Data;
using NashGrub.Models;

namespace NashGrub.Controllers
{
    public class HashtagController : Controller
    {
        private readonly NashGrubDbContext _context;

        public HashtagController(NashGrubDbContext context)
        {
            _context = context;
        }

        // GET: Hashtag
        public async Task<IActionResult> Index()
        {
            return View(await _context.Hashtags.ToListAsync());
        }

        // GET: Hashtag/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hashtag = await _context.Hashtags
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hashtag == null)
            {
                return NotFound();
            }

            return View(hashtag);
        }

        // GET: Hashtag/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Hashtag/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BusinessName")] Hashtag hashtag)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hashtag);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hashtag);
        }

        // GET: Hashtag/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hashtag = await _context.Hashtags.FindAsync(id);
            if (hashtag == null)
            {
                return NotFound();
            }
            return View(hashtag);
        }

        // POST: Hashtag/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BusinessName")] Hashtag hashtag)
        {
            if (id != hashtag.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hashtag);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HashtagExists(hashtag.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(hashtag);
        }

        // GET: Hashtag/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hashtag = await _context.Hashtags
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hashtag == null)
            {
                return NotFound();
            }

            return View(hashtag);
        }

        // POST: Hashtag/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hashtag = await _context.Hashtags.FindAsync(id);
            if (hashtag != null)
            {
                _context.Hashtags.Remove(hashtag);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HashtagExists(int id)
        {
            return _context.Hashtags.Any(e => e.Id == id);
        }
    }
}
