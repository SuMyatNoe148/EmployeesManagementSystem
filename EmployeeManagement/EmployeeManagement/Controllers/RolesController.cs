using EmployeeManagement.Services;
using EmployeeManagement.Data;
using EmployeeManagement.Models;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace EmployeeManagement.Controllers
{
    public class RolesController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public RolesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _context = context;
        }
        public async Task<ActionResult> Index()
        {
            var roles = await _context.Roles.ToListAsync();
            return View(roles);
        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RolesViewModel model)
        {
            try
            {
                var role = new IdentityRole();

                role.Name = model.RoleName;


                var result = await _roleManager.CreateAsync(role);

                if (result.Succeeded)
                {
                    TempData["Message"] = "Role Created Successfully!";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Error"] = "Error creating Role";
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error creating Role" + ex.Message;
                return View(model);
            }
        }

        [HttpGet]
        public async Task<ActionResult> Edit(string id)
        {
            var role = new RolesViewModel();
            var result = await _roleManager.FindByIdAsync(id);
            role.RoleName = result.Name;
            role.Id = result.Id;

            return View(role);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(string id, RolesViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var checkifexit = await _roleManager.RoleExistsAsync(model.RoleName);
            if (!checkifexit)
            {
                var result = await _roleManager.FindByIdAsync(id);
                result.Name = model.RoleName;


                var finalresult = await _roleManager.UpdateAsync(result);

                if (finalresult.Succeeded)
                {
                    TempData["Message"] = "Role Updated Successful!";
                    return RedirectToAction("Index");
                }

                // ❗ VERY IMPORTANT: show errors 
                foreach (var error in finalresult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(model);
            }
            return View(model);

        }


        [HttpGet]
        public async Task<ActionResult> Delete(string id)
        {
            var role = new RolesViewModel();
            var result = await _roleManager.FindByIdAsync(id);
            role.RoleName = result.Name;
            role.Id = result.Id;

            return View(role);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(string id, RolesViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await _roleManager.FindByIdAsync(id);
            result.Name = model.RoleName;


            var finalresult = await _roleManager.DeleteAsync(result);

            if (finalresult.Succeeded)
            {
                return RedirectToAction("Index");
            }

            // ❗ VERY IMPORTANT: show errors 
            foreach (var error in finalresult.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(model);

        }
    }
}
