using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Project_Gym_Asp_Net.Models;
using Project_Gym_Asp_Net.ViewModels;


namespace Project_Gym_Asp_Net.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
      
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {

                //var user = await _userManager.FindByNameAsync(model.Email);
                //if (user != null)
                //{
                //    if (!await _userManager.IsEmailConfirmedAsync(user))
                //    {
                //        ModelState.AddModelError(string.Empty, "You do not confirmed email");
                //        return View(model);
                //    }
                //}

                // Login в identity подивитися як писати.
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Неправильний Логін або пароль.");
                }
            }
            return RedirectToAction("AllUsers", "Admin");
            //return View(model);
        }

        //public async Task<IActionResult> Login(LoginViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = await _userManager.FindByNameAsync(model.Email);
        //        var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
        //        if (result.Succeeded)
        //        {
        //            if(!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
        //            {
        //                return Redirect(model.ReturnUrl);
        //            }
        //            else
        //            {
        //               return RedirectToAction("Index", "Home");
        //            }
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", "Wrong Something!");
        //        }
        //    }            
        //    return View(model);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
