using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Gym_Asp_Net.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public virtual ICollection<Exercise> Exercises { get; set; }
        public virtual ICollection<Schedule> Schedules { get; set; }
        public Category()
        {
            Exercises = new List<Exercise>();
            Schedules = new List<Schedule>();
        }
    }
}
