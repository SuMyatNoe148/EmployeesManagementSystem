using EmployeeManagement.Services;
using EmployeeManagement.Data;
using EmployeeManagement.Models;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Claims;

namespace EmployeeManagement.Controllers
{

    public class ProfilesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProfilesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult> Index()
        {
            var tasks = new ProfileViewModel();
            var roles = await _context.Roles.OrderBy(x => x.Name).ToListAsync();
            ViewBag.Roles = new SelectList(roles, "Id", "Name");

            var systemtasks = await _context.systemProfiles
                .Include("Children.Children.Children")
                .OrderBy(x => x.Order)
                .ToListAsync();

            ViewBag.Tasks = new SelectList(systemtasks, "Id", "Name");

            return View(tasks);
        }

        public async Task<ActionResult> AssignRights(ProfileViewModel vm)
        {
            try
            {
                var Userid = User.GetUserId();
                var role = new RoleProfile
                {
                    TaskId = vm.TaskId,
                    RoleId = vm.RoleId
                };
                _context.RoleProfiles.Add(role);
                await _context.SaveChangesAsync(Userid);
                TempData["Message"] = "Role Assigned Successfully!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error assigning Role" + ex.Message;
                return View(vm);
            }
        }

        [HttpGet]
        public async Task<ActionResult> UserRights(string id)
        {
            
            var tasks = new ProfileViewModel();

            var allroles = await _context.Roles.OrderByDescending(x => x.Name).ToListAsync();
            ViewBag.RoleId = new SelectList(allroles, "Id", "Name",id);


            tasks.Profile = await _context.systemProfiles
                .Include(s => s.Profile)
                .Include("Children.Children.Children")
                .OrderBy(x => x.Order)
                .ToListAsync();

            tasks.RolesRightsIds = await _context.RoleProfiles.Where(x => x.RoleId == id).Select(r => r.TaskId).ToListAsync();

            return View(tasks);
        }

        [HttpPost]
        public async Task<ActionResult> UserGroupRights(ProfileViewModel vm)
        {
            try
            {
                var Userid = User.GetUserId();

                var allrights = await _context.RoleProfiles.Where(x => x.RoleId == vm.RoleId).ToListAsync();
                _context.RoleProfiles.RemoveRange(allrights);
                await _context.SaveChangesAsync(Userid);
                foreach (var taskId in vm.Ids)
                {
                    var role = new RoleProfile
                    {
                        TaskId = taskId,
                        RoleId = vm.RoleId
                    };
                    _context.RoleProfiles.Add(role);

                    await _context.SaveChangesAsync(Userid);
                }
                TempData["Message"] = "Rights Assigned Successful!";
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                TempData["Error"] = "Rights could not be assigned Successful!" + ex.Message;
                return RedirectToAction(nameof(UserRights));
            }
        }

    }
}
