using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
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

        public IActionResult AllUsers(Sorts sort = Sorts.UserEmailAsk)
        {
            var users = from us in userManager.Users select us;
            ViewData["EmailSort"] = sort == Sorts.UserEmailAsk ? Sorts.UserEmailDesk : Sorts.UserEmailAsk;
            users = sort switch
            {
                Sorts.UserEmailDesk => users.OrderByDescending(u => u.Email),
                _ => users.OrderBy(u => u.Email)
            };
            return View(users.ToList());
        }
    }
}
