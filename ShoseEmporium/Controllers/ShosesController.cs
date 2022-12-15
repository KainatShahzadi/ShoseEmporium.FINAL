using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShoseEmporium.Data;
using ShoseEmporium.Models;

namespace ShoseEmporium.Controllers
{
    public class ShosesController : Controller
    {
        private readonly ApplicationDbContext _context;
        

        public ShosesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Shoses
        public async Task<IActionResult> Index()
        {
            ViewData["VendorofShose"] = await _context.VendorofShose.ToListAsync();
            ViewData["Vendors"] = await _context.Vendor.ToListAsync();
            var applicationDbContext = _context.Shose.Include(b => b.Employees);
            return View(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> ShoseList()
        {
            return View(await _context.Shose.ToListAsync());
        }
        public IActionResult SearchForm()
        {
            return View();
        }
        public async Task<IActionResult> SearchResult(string Title)
        {
            ViewData["VendorofShose"] = await _context.VendorofShose.ToListAsync();
            ViewData["Vendors"] = await _context.Vendor.ToListAsync();
            var applicationDbContext = _context.Shose.Include(f => f.Employees);
            return View("Index", await _context.Shose.Where(a => a.Title.Contains(Title)).ToListAsync());
        }
        public async Task<IActionResult> ShoseDetails(int? Id)
        {
            ViewData["VendorofShose"] = await _context.VendorofShose.ToListAsync();
            ViewData["Vendors"] = await _context.Vendor.ToListAsync();
            var applicationDbContext = _context.Shose.Include(f => f.Employees);
            return View("Index", await _context.Shose.Where(a => a.Id == Id).ToListAsync());
        }


        // GET: Shoses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shose = await _context.Shose
                 .Include(b => b.Employees)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shose == null)
            {
                return NotFound();
            }

            return View(shose);
        }

        // GET: Shoses/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "Name");
            ViewData["VendorId"] = new SelectList(_context.Vendor, "Id", "Name");
            return View();
        }

        // POST: Shoses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,URL,Price,EmployeeId")] Shose shose, List<int> Vendors)
        {
            if (ModelState.IsValid)
            {
                _context.Shose.Add(shose);
                await _context.SaveChangesAsync();
                List<VendorofShose> vendorofShose = new List<VendorofShose>();
                foreach (int vendor in Vendors)
                {
                    vendorofShose.Add(new VendorofShose { VendorId = vendor, ShoseId = shose.Id });
                }
                _context.VendorofShose.AddRange(vendorofShose);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "Id", shose.EmployeeId);
            return View(shose);
        }

        // GET: Shoses/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shose = await _context.Shose.FindAsync(id);
            if (shose == null)
            {
                return NotFound();
            }
            IList<VendorofShose> vendorofShoses = await _context.VendorofShose.Where<VendorofShose>(a => a.ShoseId == shose.Id).ToListAsync();
            IList<int> listVendors = new List<int>();
            foreach (VendorofShose vendorofShose in vendorofShoses)
            {
                listVendors.Add(vendorofShose.VendorId);
            }



            ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "Name", shose.EmployeeId);
            ViewData["VendorId"] = new MultiSelectList(_context.Vendor, "Id", "Name", listVendors.ToArray());
            return View(shose);
        }

        // POST: Shoses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,URL,Price,EmployeeId")] Shose shose)
        {
            if (id != shose.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shose);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShoseExists(shose.Id))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "Id", shose.EmployeeId);
            return View(shose);
        }
        

        // GET: Shoses/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shose = await _context.Shose
                .Include(b => b.Employees)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shose == null)
            {
                return NotFound();
            }

            return View(shose);
        }

        // POST: Shoses/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shose = await _context.Shose.FindAsync(id);
            _context.Shose.Remove(shose);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShoseExists(int id)
        {
            return _context.Shose.Any(e => e.Id == id);
        }
    }
}
