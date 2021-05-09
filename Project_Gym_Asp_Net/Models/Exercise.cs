using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Gym_Asp_Net.Models
{
    public class Exercise
    {
        public int Id { get; set; }
        public string ExerciseName { get; set; }
        public string CountApproach { get; set; }
        public string Repeats { get; set; }
        public virtual Category Category { get; set; }
    }
}
