using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Web.Controllers
{
    [Authorize(Roles = "Admin, User")]
    public class RoleManagerController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleManagerController(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            return View(_userManager.Users.ToList());
        }

        public async Task<IActionResult> Details(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            ViewBag.Roles = new MultiSelectList(_roleManager.Roles, "Name", "Name", await _userManager.GetRolesAsync(user));
            
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Save(string userId, IEnumerable<string> roles)
        {
            var user = await _userManager.FindByIdAsync(userId);
            await _userManager.RemoveFromRolesAsync(user, _roleManager.Roles.Select(x => x.Name));

            await _userManager.AddToRolesAsync(user, roles);
            return RedirectToAction(nameof(Index));
        }
    }
}
