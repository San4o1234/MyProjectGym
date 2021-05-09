using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Project_Gym_Asp_Net.Models;
using Project_Gym_Asp_Net.ViewModels;

namespace Project_Gym_Asp_Net.Controllers
{
    public class RolesController : Controller
    {
        RoleManager<IdentityRole> roleManager;
        UserManager<User> userManager;
        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public IActionResult AllRoles()
        {
            return View(roleManager.Roles.ToList());
        }

        public IActionResult UserList()
        {
            return View(userManager.Users.ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            if (!String.IsNullOrEmpty(name))
            {
                IdentityResult result = await roleManager.CreateAsync(new IdentityRole(name));
                if (result.Succeeded)
                {
                    return RedirectToAction("AllRoles", "Roles");
                }
                else
                {
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(name);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            IdentityRole role = await roleManager.FindByIdAsync(id);
            if(role != null)
            {
                IdentityResult result = await roleManager.DeleteAsync(role);
            }
            return RedirectToAction("AllRoles", "Roles");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string userId)
        {
            // получаєм User
            User user = await userManager.FindByIdAsync(userId);
            if (user != null)
            {
                // получаєм список ролей користувача
                var userRoles = await userManager.GetRolesAsync(user);
                var allRoles = roleManager.Roles.ToList();
                ChangeRoleViewModel model = new ChangeRoleViewModel
                {
                    UserId = user.Id,
                    UserEmail = user.Email,
                    AllRoles = allRoles, 
                    UserRoles = userRoles
                };
                return View(model);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string userId, List<string> roles)
        {
            // получаєм користувача
            User user = await userManager.FindByIdAsync(userId);
            if (user != null)
            {
                //получаєм список ролей користувача
                var userRoles = await userManager.GetRolesAsync(user);
                //получаєм всі ролі.
                var allRoles = roleManager.Roles.ToList();
                //получаєм список ролей які були добавлені
                var addedRoles = roles.Except(userRoles);
                //получаєм список ролей які були видалені
                var removedRoles = userRoles.Except(roles);

                await userManager.AddToRolesAsync(user, addedRoles);
                await userManager.RemoveFromRolesAsync(user, removedRoles);
                return RedirectToAction("UserList", "Roles");
            }
            return NotFound();
        }
    }
}
