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
    [Authorize(Roles = "admin")]
    public class AdminController: Controller
    {
        UserManager<User> userManager;
        public AdminController(UserManager<User> manager)
        {
            userManager = manager;
        }

        public IActionResult AllUsers()
        {
            return View(userManager.Users.ToList());
        }
    }
}
