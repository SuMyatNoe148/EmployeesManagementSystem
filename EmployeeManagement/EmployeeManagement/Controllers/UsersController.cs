using EmployeeManagement.Services;
using EmployeeManagement.Data;
using EmployeeManagement.Models;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EmployeeManagement.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;


        public UsersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _context = context;
        }
        public async Task<ActionResult> Index()
        {
            var users = await _context.Users.Include(x => x.Role).ToListAsync();
            return View(users);
        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserViewModel model)
        {
            try
            {
                var user = new ApplicationUser
                {
                    UserName = model.UserName,
                    FirstName = model.FirstName,
                    MiddleName = model.MiddleName,
                    LastName = model.LastName,
                    NationalId = model.NationalId,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber.ToString(),
                    EmailConfirmed = true,
                    CreatedById = User.FindFirstValue(ClaimTypes.NameIdentifier),
                    CreatedOn = DateTime.Now,
                    RoleId = model.RoleId
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    TempData["Message"] = "User Created Successfully!";

                    return RedirectToAction(nameof(Index));
                }

                // SHOW REAL ERRORS
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                TempData["Error"] = "User creation failed";
            }
            catch (Exception ex)
            {
                TempData["Error"] =
                    "User creation failed: " + ex.Message;
            }

            // RELOAD DROPDOWN
            ViewData["RoleId"] = new SelectList(
                _context.Roles,
                "Id",
                "Name",
                model.RoleId);

            return View(model);
        }
    }
}
