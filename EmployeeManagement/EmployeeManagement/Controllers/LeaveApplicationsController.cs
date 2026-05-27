using EmployeeManagement.Services;
using EmployeeManagement.Data;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EmployeeManagement.Controllers
{
    public class LeaveApplicationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;


        public LeaveApplicationsController(IConfiguration configuration, ApplicationDbContext context)
        {
            _context = context;
            _configuration = configuration;

        }

        // GET: LeaveApplications
        public async Task<IActionResult> Index()
        {
            var leaveApplication = await _context.leaveApplications
                .Include(l => l.Duration)
                .Include(l => l.Employee)
                .Include(l => l.LeaveType)
                .Include(l => l.Status)
                .ToListAsync();

            return View(leaveApplication);
        }

        public async Task<IActionResult> ApprovedLeaveApplication()
        {
            var approvedstatus = _context.systemCodeDetails.Include(x => x.SystemCode).Where(y => y.Code == "Approved" && y.SystemCode.Code == "LeaveApprovalStatus").FirstOrDefault();

            var applicationDbContext = _context.leaveApplications
                .Include(l => l.Duration)
                .Include(l => l.Employee)
                .Include(l => l.LeaveType)
                .Include(l => l.Status)
                .Where(l => l.StatusId == approvedstatus!.Id);
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> RejectedLeaveApplication()
        {
            var rejectedstatus = _context.systemCodeDetails.Include(x => x.SystemCode).Where(y => y.Code == "Rejected" && y.SystemCode.Code == "LeaveApprovalStatus").FirstOrDefault();

            var applicationDbContext = _context.leaveApplications
                .Include(l => l.Duration)
                .Include(l => l.Employee)
                .Include(l => l.LeaveType)
                .Include(l => l.Status)
                .Where(l => l.StatusId == rejectedstatus!.Id);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: LeaveApplications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveApplication = await _context.leaveApplications
                .Include(l => l.Duration)
                .Include(l => l.Employee)
                .Include(l => l.LeaveType)
                .Include(l => l.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leaveApplication == null)
            {
                return NotFound();
            }

            return View(leaveApplication);
        }

        // GET: LeaveApplications/Create
        public IActionResult Create()
        {
            ViewData["DurationId"] = new SelectList(_context.systemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "LeaveDuration"), "Id", "Description");
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FullName");
            ViewData["LeaveTypeId"] = new SelectList(_context.LeaveTypes, "Id", "Name");
            return View();
        }

        // POST: LeaveApplications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LeaveApplication leaveApplication, IFormFile leaveattachment)
        {
            try
            {
                if (leaveattachment != null && leaveattachment.Length > 0)
                {
                    var fileName = "LeaveAttachment_" + DateTime.Now.ToString("yyyymmddhhmmss") + "_" + leaveattachment.FileName;
                    var path = _configuration["FileSettings:UploadFolder"]!;
                    var filePath = Path.Combine(path, fileName);
                    var stream = new FileStream(filePath, FileMode.Create);
                    await leaveattachment.CopyToAsync(stream);
                    leaveApplication.Attachment = fileName;
                }

                leaveApplication.EndDate = leaveApplication.StartDate.AddDays(leaveApplication.NoOfDays);


                var pendingStatus = await _context.systemCodeDetails
                    .Include(x => x.SystemCode)
                    .Where(y => y.Code == "AwaitingApproval" && y.SystemCode.Code == "LeaveApprovalStatus")
                    .FirstOrDefaultAsync();

                if (pendingStatus == null)
                {
                    ModelState.AddModelError("", "Pending status not found.");
                }

                var Userid = User.GetUserId();
                leaveApplication.CreatedOn = DateTime.Now;
                leaveApplication.CreatedById = Userid;
                leaveApplication.StatusId = pendingStatus.Id;

                _context.Add(leaveApplication);
                await _context.SaveChangesAsync(Userid);
                TempData["Message"] = "Leave Application Created Successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occured while creating Leave Application" + ex.Message;

            }

            //  RELOAD DROPDOWNS 
            ViewData["DurationId"] = new SelectList(
                _context.systemCodeDetails.Include(x => x.SystemCode)
                .Where(y => y.SystemCode.Code == "LeaveDuration"),
                "Id", "Description", leaveApplication.DurationId);

            ViewData["EmployeeId"] = new SelectList(
                _context.Employees, "Id", "FullName", leaveApplication.EmployeeId);

            ViewData["LeaveTypeId"] = new SelectList(
                _context.LeaveTypes, "Id", "Name", leaveApplication.LeaveTypeId);
            return View(leaveApplication);

        }

        // GET: LeaveApplications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveApplication = await _context.leaveApplications.FindAsync(id);
            if (leaveApplication == null)
            {
                return NotFound();
            }
            ViewData["DurationId"] = new SelectList(_context.systemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "LeaveDuration"), "Id", "Description", leaveApplication.DurationId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FullName", leaveApplication.EmployeeId);
            ViewData["LeaveTypeId"] = new SelectList(_context.LeaveTypes, "Id", "Name", leaveApplication.LeaveTypeId);
            return View(leaveApplication);
        }

        // POST: LeaveApplications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LeaveApplication leaveApplication)
        {
            if (id != leaveApplication.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var pendingStatus = await _context.systemCodeDetails.Include(x => x.SystemCode).Where(y => y.Code == "Pending" && y.SystemCode.Code == "LeaveApprovalStatus").FirstOrDefaultAsync();

                try
                {
                    leaveApplication.ModifiedOn = DateTime.Now;
                    leaveApplication.ModifiedById = "Marco Code";
                    leaveApplication.StatusId = pendingStatus.Id;
                    _context.Update(leaveApplication);
                    TempData["Message"] = "Leave Application Updated Successful!";
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeaveApplicationExists(leaveApplication.Id))
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
            ViewData["DurationId"] = new SelectList(_context.systemCodeDetails, "Id", "Description", leaveApplication.DurationId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FullName", leaveApplication.EmployeeId);
            ViewData["LeaveTypeId"] = new SelectList(_context.LeaveTypes, "Id", "Name", leaveApplication.LeaveTypeId);
            return View(leaveApplication);
        }

        // GET: LeaveApplications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveApplication = await _context.leaveApplications
                .Include(l => l.Duration)
                .Include(l => l.Employee)
                .Include(l => l.LeaveType)
                .Include(l => l.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leaveApplication == null)
            {
                return NotFound();
            }

            return View(leaveApplication);
        }

        // POST: LeaveApplications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var leaveApplication = await _context.leaveApplications.FindAsync(id);
            if (leaveApplication != null)
            {
                _context.leaveApplications.Remove(leaveApplication);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> ApproveLeave(int id)
        {
            var leaveApplication = await _context.leaveApplications
                .Include(l => l.Duration)
                .Include(l => l.Employee)
                .Include(l => l.LeaveType)
                .Include(l => l.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leaveApplication == null)
            {
                return NotFound();
            }

            ViewData["DurationId"] = new SelectList(_context.systemCodeDetails, "Id", "Description", leaveApplication.DurationId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FullName", leaveApplication.EmployeeId);
            ViewData["LeaveTypeId"] = new SelectList(_context.LeaveTypes, "Id", "Name", leaveApplication.LeaveTypeId);
            return View(leaveApplication);
        }

        [HttpPost]
        public async Task<IActionResult> ApproveLeave(LeaveApplication leave)
        {
            try
            {
                var approvedstatus = _context.systemCodeDetails
                    .Include(x => x.SystemCode)
                    .Where(y => y.Code == "Approved" && y.SystemCode.Code == "LeaveApprovalStatus")
                    .FirstOrDefault();
                var adjustmenttype = _context.systemCodeDetails
                    .Include(x => x.SystemCode)
                    .Where(y => y.Code == "Negative" && y.SystemCode.Code == "LeaveAdjustment")
                    .FirstOrDefault();

                var leaveApplication = await _context.leaveApplications
                    .Include(l => l.Duration)
                    .Include(l => l.Employee)
                    .Include(l => l.LeaveType)
                    .Include(l => l.Status)
                    .FirstOrDefaultAsync(m => m.Id == leave.Id);
                if (leaveApplication == null)
                {
                    return NotFound();
                }

                var Userid = User.GetUserId();
                leaveApplication.ApprovedOn = DateTime.Now;
                leaveApplication.ApprovedById = Userid;
                leaveApplication.StatusId = approvedstatus!.Id;
                leaveApplication.ApprovalNotes = leave.ApprovalNotes;


                _context.Update(leaveApplication);
                await _context.SaveChangesAsync(Userid);

                var adjustment = new LeaveAdjustmentEntry
                {
                    EmployeeId = leaveApplication.EmployeeId,
                    NoOfDays = leaveApplication.NoOfDays,
                    LeaveStartDate = leaveApplication.StartDate,
                    LeaveEndDate = leaveApplication.EndDate,
                    AdjustmentDescription = "Leave Taken-Negative Adjustment",
                    LeavePeriodId = null,
                    LeaveAdjustmentDate = DateTime.Now,
                    AdjustmentTypeId = adjustmenttype.Id
                };
                _context.Add(adjustment);
                await _context.SaveChangesAsync(Userid);

                var employee = await _context.Employees.FindAsync(leaveApplication.EmployeeId);
                employee.LeaveOutstandingBalance = (employee.AllocatedLeaveDays - leaveApplication.NoOfDays);
                _context.Update(employee);
                await _context.SaveChangesAsync(Userid);

                TempData["Message"] = "Leave Application Approved Successful!";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Leave Application could not be approved successful" + ex.Message;
            }
            ViewData["DurationId"] = new SelectList(_context.systemCodeDetails, "Id", "Description", leave.DurationId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FullName", leave.EmployeeId);
            ViewData["LeaveTypeId"] = new SelectList(_context.LeaveTypes, "Id", "Name", leave.LeaveTypeId);
            return View(leave);
        }

        [HttpGet]
        public async Task<IActionResult> RejectLeave(int id)
        {
            var leaveApplication = await _context.leaveApplications
                .Include(l => l.Duration)
                .Include(l => l.Employee)
                .Include(l => l.LeaveType)
                .Include(l => l.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leaveApplication == null)
            {
                return NotFound();
            }

            ViewData["DurationId"] = new SelectList(_context.systemCodeDetails, "Id", "Description", leaveApplication.DurationId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FullName", leaveApplication.EmployeeId);
            ViewData["LeaveTypeId"] = new SelectList(_context.LeaveTypes, "Id", "Name", leaveApplication.LeaveTypeId);
            return View(leaveApplication);
        }

        [HttpPost]
        public async Task<IActionResult> RejectLeave(LeaveApplication leave)
        {
            try
            {

                var rejectedstatus = _context.systemCodeDetails.Include(x => x.SystemCode).Where(y => y.Code == "Rejected" && y.SystemCode.Code == "LeaveApprovalStatus").FirstOrDefault();

                var leaveApplication = await _context.leaveApplications
                    .Include(l => l.Duration)
                    .Include(l => l.Employee)
                    .Include(l => l.LeaveType)
                    .Include(l => l.Status)
                    .FirstOrDefaultAsync(m => m.Id == leave.Id);
                if (leaveApplication == null)
                {
                    return NotFound();
                }

                leaveApplication.ApprovedOn = DateTime.Now;
                leaveApplication.ApprovedById = "Marco Code";
                leaveApplication.StatusId = rejectedstatus!.Id;
                leaveApplication.ApprovalNotes = leave.ApprovalNotes;


                _context.Update(leaveApplication);
                await _context.SaveChangesAsync();
                TempData["Message"] = "Leave Application Rejected Successful!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Leave Application could not be rejected Successful!" + ex.Message;
            }

            ViewData["DurationId"] = new SelectList(_context.systemCodeDetails, "Id", "Description", leave.DurationId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FullName", leave.EmployeeId);
            ViewData["LeaveTypeId"] = new SelectList(_context.LeaveTypes, "Id", "Name", leave.LeaveTypeId);
            return View(leave);
        }
        private bool LeaveApplicationExists(int id)
        {
            return _context.leaveApplications.Any(e => e.Id == id);
        }
    }
}
