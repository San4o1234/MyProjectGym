using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace Project_Gym_Asp_Net.Models
{
    public class MyContext: IdentityDbContext<User>
    {
        public MyContext(DbContextOptions<MyContext> options): base(options)
        {
            Database.EnsureCreated();
        }

        //public DbSet<User> Users { get; set; }
        public virtual DbSet<Schedule> Schedules { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Exercise> Exercises { get; set; }

        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    builder.Entity<Schedule>().HasMany(s => s.Categories).WithMany(c => c.Schedules);

        //    //base.OnModelCreating(builder);
        //}
    }
}
