using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Project_Gym_Asp_Net.Models;
using Project_Gym_Asp_Net.ViewModels;


namespace Project_Gym_Asp_Net.Controllers
{
   // [Authorize(Roles = "admin")]
    public class UsersController : Controller
    {
        UserManager<User> userManager;
        public UsersController(UserManager<User> manager)
        {
            userManager = manager;
        }
                
        //public IActionResult AllUsers()
        //{
        //    return View(userManager.Users.ToList());
        //}

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User { Email = model.Email, UserName = model.UserName, PhoneNumber = model.Phone };
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("AllUsers", "Admin");
                }
                else
                {
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            User user = await userManager.FindByIdAsync(id);
            if(user == null)
            {
                return NotFound();
            }
            EditUserViewModel editUser = new EditUserViewModel { Id = user.Id, Email = user.Email, UserName = user.UserName, Phone = user.PhoneNumber };
            return View(editUser);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await userManager.FindByIdAsync(model.Id);
                if(user != null)
                {
                    user.Email = model.Email;
                    user.UserName = model.UserName;
                    user.PhoneNumber = model.Phone;

                    var result = await userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("AllUsers", "Admin");
                    }
                    else
                    {
                        foreach(var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            User user = await userManager.FindByIdAsync(id);
            if(user != null)
            {
                IdentityResult result = await userManager.DeleteAsync(user);
            }
            return RedirectToAction("AllUsers", "Admin");
        }

        [HttpGet]
        public async Task<IActionResult> ChangePassword(string id)
        {
            User user = await userManager.FindByIdAsync(id);
            if(user == null)
            {
                return NotFound();
            }
            ChangePasswordViewModel model = new ChangePasswordViewModel { Id = user.Id, Email = user.Email };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await userManager.FindByIdAsync(model.Id);
                if(user != null)
                {
                    IdentityResult result = await userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("AllUsers", "Admin");
                    }
                    else
                    {
                        foreach(var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "User Not found");
                }
            }
            return View(model);
        }
    }
}
