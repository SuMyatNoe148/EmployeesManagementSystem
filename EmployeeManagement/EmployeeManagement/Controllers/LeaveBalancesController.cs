using EmployeeManagement.Services;
using EmployeeManagement.Data;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EmployeeManagement.Controllers
{
    public class LeaveBalancesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public LeaveBalancesController( ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ActionResult> Index()
        {
            var result = await _context.Employees
                .Include(x => x.Status).ToListAsync();

            return View(result);
        }

        [HttpGet]
        public IActionResult AdjustLeaveBalance(int id)
        {
            LeaveAdjustmentEntry leaveAdjustment = new();
            leaveAdjustment.EmployeeId = id;
            ViewData["AdjustmentTypeId"] = new SelectList(_context.systemCodeDetails.Include(y=>y.SystemCode).Where(x=>x.SystemCode.Code=="LeaveAdjustment"), "Id", "Description");
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FullName",id);
            ViewData["LeavePeriodId"] = new SelectList(_context.LeavePeriods.Where(x=>x.Closed ==false), "Id", "Name");


            return View(leaveAdjustment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdjustLeaveBalance(LeaveAdjustmentEntry leaveAdjustmentEntry)
        {
            try
            {
                var adjustmenttype = _context.systemCodeDetails
                    .Include(x => x.SystemCode)
                    .Where(y => y.Id == leaveAdjustmentEntry.AdjustmentTypeId && y.SystemCode.Code == "LeaveAdjustment")
                    .FirstOrDefault();

                leaveAdjustmentEntry.AdjustmentDescription = leaveAdjustmentEntry.AdjustmentDescription + "-" + adjustmenttype.Description;
                leaveAdjustmentEntry.Id = 0;
                var Userid = User.GetUserId();
                _context.Add(leaveAdjustmentEntry);
                await _context.SaveChangesAsync(Userid);

                var employee = await _context.Employees.FindAsync(leaveAdjustmentEntry.EmployeeId);
                if (adjustmenttype.Code == "Positive")
                {
                    employee.LeaveOutstandingBalance += leaveAdjustmentEntry.NoOfDays;

                }
                else
                {
                    employee.LeaveOutstandingBalance -= leaveAdjustmentEntry.NoOfDays;

                }
                _context.Update(employee);
                await _context.SaveChangesAsync(Userid);
                TempData["Message"] = "Leave Balance Adjusted Successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error adjusting Leave Balance" + ex.Message;
                return View(leaveAdjustmentEntry);
            }
            ViewData["LeavePeriodId"] = new SelectList(_context.LeavePeriods.Where(x => x.Closed == false), "Id", "Name",leaveAdjustmentEntry.LeavePeriodId);
            ViewData["AdjustmentTypeId"] = new SelectList(_context.systemCodeDetails, "Id", "Description", leaveAdjustmentEntry.AdjustmentTypeId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FullName", leaveAdjustmentEntry.EmployeeId);
            
        }
    }
}
