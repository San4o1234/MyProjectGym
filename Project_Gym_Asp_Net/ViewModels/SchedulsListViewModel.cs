using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project_Gym_Asp_Net.Models;

namespace Project_Gym_Asp_Net.ViewModels
{
    public class SchedulsListViewModel
    {
        public SelectList SchedulsSL { get; set; }
        public SelectList CategoriesSL { get; set; }
        public IEnumerable<Exercise> ExercisesSL { get; set; }
    }
}
