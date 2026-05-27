using EmployeeManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Data;
using EmployeeManagement.Models;
using System.Security.Claims;

namespace EmployeeManagement.Controllers
{
    public class LeaveAdjustmentEntriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LeaveAdjustmentEntriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LeaveAdjustmentEntries
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.LeaveAdjustmentEntries
                .Include(x => x.CreatedBy)
                .Include(x => x.ModifiedBy).Include(l => l.AdjustmentType).Include(l => l.Employee);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: LeaveAdjustmentEntries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveAdjustmentEntry = await _context.LeaveAdjustmentEntries
                .Include(l => l.AdjustmentType)
                .Include(l => l.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leaveAdjustmentEntry == null)
            {
                return NotFound();
            }

            return View(leaveAdjustmentEntry);
        }

        // GET: LeaveAdjustmentEntries/Create
        public IActionResult Create()
        {
            ViewData["AdjustmentTypeId"] = new SelectList(
                _context.systemCodeDetails
                    .Include(x => x.SystemCode)
                    .Where(x => x.SystemCode.Code == "LeaveAdjustment"),
                "Id",
                "Description");

            ViewData["EmployeeId"] = new SelectList(
                _context.Employees,
                "Id",
                "FullName");

            ViewData["LeavePeriodId"] = new SelectList(
                _context.LeavePeriods.Where(x => x.Closed == false),
                "Id",
                "Name");

            return View();
        }

        // POST: LeaveAdjustmentEntries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LeaveAdjustmentEntry leaveAdjustmentEntry)
        {
            try
            {
                var userId = User.GetUserId();

                leaveAdjustmentEntry.CreatedById = userId;
                leaveAdjustmentEntry.CreatedOn = DateTime.Now;

                _context.LeaveAdjustmentEntries.Add(leaveAdjustmentEntry);

                await _context.SaveChangesAsync(userId);

                TempData["Message"] = "Leave Adjustment Entry Created Successfully!";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.InnerException?.Message ?? ex.Message;
            }

            ViewData["AdjustmentTypeId"] = new SelectList(
                _context.systemCodeDetails
                    .Include(x => x.SystemCode)
                    .Where(x => x.SystemCode.Code == "LeaveAdjustment"),
                "Id",
                "Description",
                leaveAdjustmentEntry.AdjustmentTypeId);

            ViewData["EmployeeId"] = new SelectList(
                _context.Employees,
                "Id",
                "FullName",
                leaveAdjustmentEntry.EmployeeId);

            ViewData["LeavePeriodId"] = new SelectList(
                _context.LeavePeriods.Where(x => x.Closed == false),
                "Id",
                "Name",
                leaveAdjustmentEntry.LeavePeriodId);

            return View(leaveAdjustmentEntry);
        }
        // GET: LeaveAdjustmentEntries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveAdjustmentEntry = await _context.LeaveAdjustmentEntries.FirstOrDefaultAsync(x => x.Id == id);
            if (leaveAdjustmentEntry == null)
            {
                return NotFound();
            }
            ViewData["AdjustmentTypeId"] = new SelectList(_context.systemCodeDetails, "Id", "Description", leaveAdjustmentEntry.AdjustmentTypeId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FullName", leaveAdjustmentEntry.EmployeeId);
            return View(leaveAdjustmentEntry);
        }

        // POST: LeaveAdjustmentEntries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LeaveAdjustmentEntry leaveAdjustmentEntry)
        {
            if (id != leaveAdjustmentEntry.Id)
            {
                return NotFound();
            }
            var Userid = User.GetUserId();

            leaveAdjustmentEntry.ModifiedById = Userid;
            leaveAdjustmentEntry.ModifiedOn = DateTime.Now;
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(leaveAdjustmentEntry);
                    TempData["Message"] = "Leave Adjustment Entry Updated Successful!";
                    await _context.SaveChangesAsync(Userid);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeaveAdjustmentEntryExists(leaveAdjustmentEntry.Id))
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
            ViewData["AdjustmentTypeId"] = new SelectList(_context.systemCodeDetails, "Id", "Description", leaveAdjustmentEntry.AdjustmentTypeId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FullName", leaveAdjustmentEntry.EmployeeId);
            return View(leaveAdjustmentEntry);
        }

        // GET: LeaveAdjustmentEntries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveAdjustmentEntry = await _context.LeaveAdjustmentEntries
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leaveAdjustmentEntry == null)
            {
                return NotFound();
            }

            return View(leaveAdjustmentEntry);
        }

        // POST: LeaveAdjustmentEntries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Userid = User.GetUserId();
            var leaveAdjustmentEntry = await _context.LeaveAdjustmentEntries.FindAsync(id);
            if (leaveAdjustmentEntry != null)
            {
                _context.LeaveAdjustmentEntries.Remove(leaveAdjustmentEntry);
            }

            await _context.SaveChangesAsync(Userid);
            return RedirectToAction(nameof(Index));
        }

        private bool LeaveAdjustmentEntryExists(int id)
        {
            return _context.LeaveAdjustmentEntries.Any(e => e.Id == id);
        }
    }
}
