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
using EmployeeManagement.ViewModels;
using AutoMapper;
using EmployeeManagement.Services;

namespace EmployeeManagement.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public readonly IExtensionService _extension;

        public EmployeesController(IExtensionService extension, IMapper mapper, IConfiguration configuration, ApplicationDbContext context)
        {
            _context = context;
            _configuration = configuration;
            _mapper = mapper;
            _extension = extension;
        }

        // GET: Employees
        public async Task<IActionResult> Index(EmployeeViewModel model)
        {
            var rawdata = _context.Employees
                .Include(x => x.Status).AsQueryable();

            if (!string.IsNullOrEmpty(model.FullName))
            {
                rawdata = rawdata
                    .Where(x =>
                    (x.FirstName + " " + x.LastName)
                    .Contains(model.FullName.Trim()));
            }
            if (model.PhoneNumber > 0)
            {
                rawdata = rawdata
                    .Where(x => x.PhoneNumber == model.PhoneNumber);
            }
            if (!string.IsNullOrEmpty(model.EmailAddress))
            {
                rawdata = rawdata
                    .Where(x => x.EmailAddress == model.EmailAddress);
            }
            if (!string.IsNullOrEmpty(model.EmpNo))
            {
                rawdata = rawdata
                    .Where(x => x.EmpNo == model.EmpNo);
            }
            model.Employees = await rawdata.OrderBy(x => x.Id).ToListAsync();
            return View(model);
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            ViewData["BankId"] = new SelectList(_context.Banks, "Id", "Name");
            ViewData["EmploymentTermsId"] = new SelectList(_context.systemCodeDetails.Include(x => x.SystemCode).Where(x => x.SystemCode.Code == "EmploymentTerms"), "Id", "Description");
            ViewData["DisabilityId"] = new SelectList(_context.systemCodeDetails.Include(x => x.SystemCode).Where(x => x.SystemCode.Code == "DisabilityTypes"), "Id", "Description");

            ViewData["GenderId"] = new SelectList(_context.systemCodeDetails.Include(x => x.SystemCode).Where(x => x.SystemCode.Code == "Gender"), "Id", "Description");
            ViewData["CountryId"] = new SelectList(_context.countries, "Id", "Name");
            ViewData["DesignationId"] = new SelectList(_context.Designations, "Id", "Name");
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeViewModel newemployee, IFormFile employeephoto)
        {
            try
            {
                var employee = new Employee();
                _mapper.Map(newemployee, employee);

                employee.EmpNo = await _extension.GenerateEmployeeNumber();

                if (employeephoto != null && employeephoto.Length > 0)
                {
                    var fileName = "EmployeePhoto_" + DateTime.Now.ToString("yyyymmddhhmmss") + "_" + employeephoto.FileName;
                    var path = _configuration["FileSettings:UploadFolder"]!;
                    var filePath = Path.Combine(path, fileName);
                    var stream = new FileStream(filePath, FileMode.Create);
                    await employeephoto.CopyToAsync(stream);
                    employee.Photo = fileName;
                }

                var systemCode = await _context.SystemCodes
            .FirstOrDefaultAsync(x => x.Code == "EmployeeStatus");

                if (systemCode == null)
                    throw new Exception("EmployeeStatus not found");

                var statusId = await _context.systemCodeDetails
                    .FirstOrDefaultAsync(x =>
                        x.SystemCodeId == systemCode.Id && x.Code == "Active");

                if (statusId == null)
                    throw new Exception("Active status not found");


                var Userid = User.GetUserId();

                employee.CreatedById = Userid;
                employee.CreatedOn = DateTime.Now;
                employee.StatusId = statusId.Id;
                _context.Add(employee);
                await _context.SaveChangesAsync(Userid);

                TempData["Message"] = "Employees Created Successfully!";

                return RedirectToAction(nameof(Index));

                ViewData["BankId"] = new SelectList(_context.Banks, "Id", "Name", employee.BankId);
                ViewData["DisabilityId"] = new SelectList(_context.systemCodeDetails.Include(x => x.SystemCode).Where(x => x.SystemCode.Code == "DisabilityTypes"), "Id", "Description", employee.DisabilityId);
                ViewData["EmploymentTermsId"] = new SelectList(_context.systemCodeDetails.Include(x => x.SystemCode).Where(x => x.SystemCode.Code == "EmploymentTerms"), "Id", "Description", employee.EmploymentTermsId);
                ViewData["GenderId"] = new SelectList(
                   _context.systemCodeDetails.Include(x => x.SystemCode).Where(x => x.SystemCode.Code == "Gender"),
                   "Id", "Description", employee.GenderId);
                ViewData["CountryId"] = new SelectList(
                   _context.countries,
                   "Id", "Name", employee.CountryId);

                ViewData["DesignationId"] = new SelectList(
                    _context.Designations, "Id", "Name", employee.DesignationId);

                ViewData["DepartmentId"] = new SelectList(
                    _context.Departments, "Id", "Name", employee.DepartmentId);
                return View(employee);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Employees could not be Created Successfully-" + ex.Message;
                return View(newemployee);
            }
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            var newemployee = new EmployeeViewModel();
            _mapper.Map(employee, newemployee);

            ViewData["DisabilityId"] = new SelectList(_context.systemCodeDetails.Include(x => x.SystemCode).Where(x => x.SystemCode.Code == "DisabilityTypes"), "Id", "Description", employee.DisabilityId);
            ViewData["EmploymentTermsId"] = new SelectList(_context.systemCodeDetails.Include(x => x.SystemCode).Where(x => x.SystemCode.Code == "EmploymentTerms"), "Id", "Description", employee.EmploymentTermsId);
            ViewData["GenderId"] = new SelectList(
               _context.systemCodeDetails.Include(x => x.SystemCode).Where(x => x.SystemCode.Code == "Gender"),
               "Id", "Description", employee.GenderId);
            ViewData["CountryId"] = new SelectList(
               _context.countries,
               "Id", "Name", employee.CountryId);

            ViewData["DesignationId"] = new SelectList(
                _context.Designations, "Id", "Name", employee.DesignationId);

            ViewData["DepartmentId"] = new SelectList(
                _context.Departments, "Id", "Name", employee.DepartmentId);
            ViewData["BankId"] = new SelectList(
                _context.Banks, "Id", "Name", employee.BankId);
            return View(newemployee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EmployeeViewModel newemployee)
        {
            if (id != newemployee.Id)
            {
                return NotFound();
            }

            var employee = new Employee();
            _mapper.Map(newemployee, employee);

            var Userid = User.GetUserId();
            employee.ModifiedById = Userid;
            employee.ModifiedOn = DateTime.Now;
            try
            {
                _context.Update(employee);
                TempData["Message"] = "Employee Updated Successful!";
                await _context.SaveChangesAsync(Userid);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(employee.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));


            ViewData["DisabilityId"] = new SelectList(_context.systemCodeDetails.Include(x => x.SystemCode).Where(x => x.SystemCode.Code == "DisabilityTypes"), "Id", "Description", employee.DisabilityId);
            ViewData["EmploymentTermsId"] = new SelectList(_context.systemCodeDetails.Include(x => x.SystemCode).Where(x => x.SystemCode.Code == "EmploymentTerms"), "Id", "Description", employee.EmploymentTermsId);
            ViewData["GenderId"] = new SelectList(
               _context.systemCodeDetails.Include(x => x.SystemCode).Where(x => x.SystemCode.Code == "Gender"),
               "Id", "Description", employee.GenderId);
            ViewData["CountryId"] = new SelectList(
               _context.countries,
               "Id", "Name", employee.CountryId);

            ViewData["DesignationId"] = new SelectList(
                _context.Designations, "Id", "Name", employee.DesignationId);

            ViewData["DepartmentId"] = new SelectList(
                _context.Departments, "Id", "Name", employee.DepartmentId);
            ViewData["BankId"] = new SelectList(
                _context.Banks, "Id", "Name", employee.BankId);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }

        [HttpGet]
        public async Task<IActionResult> EmployeeDocuments(EmployeeViewModel vm, int? id)
        {
            if (vm.Id == null)
            {
                return NotFound();
            }
            vm.EmployeeDocuments = new();
            vm.EmployeeDocuments = await _context.EmployeeDocuments
                .Include(x => x.DocumentType)
                .Include(x => x.CreatedBy)
                .Include(x => x.Employee)
                .Where(m => m.EmployeeId == vm.Id)
                .ToListAsync();


            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> AddEmployeeDocument(int id)
        {
            var employee = await _context.Employees
                .FirstOrDefaultAsync(x => x.Id == id);

            if (employee == null)
            {
                return NotFound();
            }

            EmployeeViewModel vm = new()
            {
                Id = employee.Id,
                EmpNo = employee.EmpNo
            };

            ViewData["DocumentTypeId"] = new SelectList(_context.systemCodeDetails
                    .Include(x => x.SystemCode)
                    .Where(x => x.SystemCode.Code == "EmployeeDocumentTypes"),"Id","Description");

            return View(vm);
        }


        public async Task<IActionResult> SubmitEmployeeDocument(EmployeeViewModel vm, IFormFile employeeattachment)
        {
            try
            {
                string filePath = "";
                if (employeeattachment != null && employeeattachment.Length > 0)
                {
                    var fileName = "EmployeeAttachmentPhoto_" + DateTime.Now.ToString("yyyymmddHHmmss") + "_" + vm.DocumentName + "_" + employeeattachment.FileName;
                    var path = _configuration["FileSettings:UploadFolder"]!;
                     filePath = Path.Combine(path, fileName);
                    var stream = new FileStream(filePath, FileMode.Create);
                    await employeeattachment.CopyToAsync(stream);
                    vm.Photo = fileName;
                }

                var empdocument = new EmployeeDocument
                {
                    DocumentName = vm.DocumentName,
                    DocumentTypeId = vm.DocumentTypeId,
                    EmployeeId = vm.Id,
                    FileExtension = Path.GetExtension(employeeattachment.FileName),
                    FileSize = employeeattachment.Length,
                    FileType = employeeattachment.ContentType,
                    FilePath = filePath,
                    UploadDate = DateTime.UtcNow,
                    ExpiryDate = vm.ExpiryDate,
                    CreatedById = User.GetUserId(),
                    CreatedOn = DateTime.UtcNow
                };


                var Userid = User.GetUserId();

                _context.Add(empdocument);
                await _context.SaveChangesAsync(Userid);

                TempData["Message"] = "Employees Document Attached Successfully!";

                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                TempData["Error"] =
                    ex.InnerException?.Message ?? ex.Message;

                return View("AddEmployeeDocument", vm);
            }
        }
    }
}
