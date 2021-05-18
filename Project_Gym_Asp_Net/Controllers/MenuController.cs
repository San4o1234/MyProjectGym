using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project_Gym_Asp_Net.Models;
using Project_Gym_Asp_Net.ViewModels;

namespace Project_Gym_Asp_Net.Controllers
{
    [Authorize(Roles = "admin, user")]
    public class MenuController : Controller
    {
        MyContext context;
        UserManager<User> userManager;
        public MenuController(UserManager<User> manager, MyContext cont)
        {
            context = cont;
            userManager = manager;
        }
        
        [HttpGet]
        public async Task<IActionResult> UserRoom()
        {
            User user = await userManager.FindByNameAsync(User.Identity.Name);
            if(user == null)
            {
                return NotFound();
            }
            ViewData["Schedules"] = user.Schedules.ToList();

            return View(user);
        }
    }
}
