using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Gym_Asp_Net.Models
{
    public class Schedule
    {
        public int Id { get; set; }
        public string Day { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Exercise> Exercises{ get; set; }
        public Schedule()
        {
            Exercises = new List<Exercise>();
        }
    }
}
