using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Project_Gym_Asp_Net.Models
{
    public class User: IdentityUser
    {
        public virtual ICollection<Schedule> Schedules { get; set; }
        public User()
        {
            Schedules = new List<Schedule>();
        }
    }
}
