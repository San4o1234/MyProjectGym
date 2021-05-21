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
        public async Task<IActionResult> UserRoom(Sorts sortOrder = Sorts.DayAsk)
        {
            User user = await userManager.FindByNameAsync(User.Identity.Name);
            if(user == null)
            {
                return NotFound();
            }

            var schedulesDay = from s in user.Schedules select s;
            ViewData["DaySort"] = sortOrder == Sorts.DayAsk ? Sorts.DayDesk : Sorts.DayAsk;
            schedulesDay = sortOrder switch
            {
                Sorts.DayDesk => schedulesDay.OrderByDescending(s => s.Day),
                _ => schedulesDay.OrderBy(s => s.Day)
            };
            ViewData["Schedules"] = schedulesDay.ToList();

            return View(user);
        }
    }
}
