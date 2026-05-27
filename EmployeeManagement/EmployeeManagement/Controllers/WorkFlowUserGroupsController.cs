using EmployeeManagement.Services;
using EmployeeManagement.Data;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EmployeeManagement.Controllers
{
    public class WorkFlowUserGroupsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WorkFlowUserGroupsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: WorkFlowUserGroups
        public async Task<IActionResult> Index()
        {
            var groups = await _context.WorkFlowUserGroups
                .Include(x => x.Department)
                .Include(x => x.DocumentType).ToListAsync();
            return View(groups);
        }

        // GET: WorkFlowUserGroups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workFlowUserGroup = await _context.WorkFlowUserGroups
                .FirstOrDefaultAsync(m => m.Id == id);
            if (workFlowUserGroup == null)
            {
                return NotFound();
            }

            return View(workFlowUserGroup);
        }

        // GET: WorkFlowUserGroups/Create
        public IActionResult Create()
        {
            ViewData["DocumentTypeId"] = new SelectList(_context.systemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "DocumentTypes"), "Id", "Description");
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name");

            return View();
        }

        // POST: WorkFlowUserGroups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(WorkFlowUserGroup workFlowUserGroup)
        {
            try
            {
                var Userid = User.GetUserId();

                _context.Add(workFlowUserGroup);
                await _context.SaveChangesAsync(Userid);

                TempData["Message"] = "WorkflowUserGroup Created Successfully!";

                return RedirectToAction(nameof(Index));
               
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occured while creation the WorkflowUserGroup" + ex.Message;
                return View(workFlowUserGroup);
            }

            ViewData["DocumentTypeId"] = new SelectList(_context.systemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "DocumentTypes"), "Id", "Description", workFlowUserGroup.DocumentTypeId);
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Description", workFlowUserGroup.DepartmentId);

        }

        // GET: WorkFlowUserGroups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workFlowUserGroup = await _context.WorkFlowUserGroups.FindAsync(id);
            if (workFlowUserGroup == null)
            {
                return NotFound();
            }
            ViewData["DocumentTypeId"] = new SelectList(_context.systemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "DocumentTypes"), "Id", "Description", workFlowUserGroup.DocumentTypeId);
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", workFlowUserGroup.DepartmentId);
            return View(workFlowUserGroup);
        }

        // POST: WorkFlowUserGroups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,WorkFlowUserGroup workFlowUserGroup)
        {
            if (id != workFlowUserGroup.Id)
            {
                return NotFound();
            }
            try
            {
                var Userid = User.GetUserId();
                _context.Update(workFlowUserGroup);
                TempData["Message"] = "Workflow User Group Updated Successful!";
                await _context.SaveChangesAsync(Userid);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkFlowUserGroupExists(workFlowUserGroup.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            ViewData["DocumentTypeId"] = new SelectList(_context.systemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "DocumentTypes"), "Id", "Description",workFlowUserGroup.DocumentTypeId);
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name",workFlowUserGroup.DepartmentId);
            return RedirectToAction(nameof(Index));

            return View(workFlowUserGroup);
        }

        // GET: WorkFlowUserGroups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workFlowUserGroup = await _context.WorkFlowUserGroups
                .FirstOrDefaultAsync(m => m.Id == id);
            if (workFlowUserGroup == null)
            {
                return NotFound();
            }

            return View(workFlowUserGroup);
        }

        // POST: WorkFlowUserGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Userid = User.GetUserId();

            var workFlowUserGroup = await _context.WorkFlowUserGroups.FindAsync(id);
            if (workFlowUserGroup != null)
            {
                _context.WorkFlowUserGroups.Remove(workFlowUserGroup);
            }

            await _context.SaveChangesAsync(Userid);
            return RedirectToAction(nameof(Index));
        }

        private bool WorkFlowUserGroupExists(int id)
        {
            return _context.WorkFlowUserGroups.Any(e => e.Id == id);
        }
    }
}
