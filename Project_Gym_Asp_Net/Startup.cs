using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Proxies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Project_Gym_Asp_Net.Models;

namespace Project_Gym_Asp_Net
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //aZ#123456
            services.AddDbContext<MyContext>(o => o.UseLazyLoadingProxies().UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            //services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<MyContext>();
            services.AddIdentity<User, IdentityRole>(opt =>
           {
               opt.SignIn.RequireConfirmedEmail = false;
               opt.Password.RequiredLength = 6;
               opt.Password.RequireNonAlphanumeric = false;
               opt.Password.RequireUppercase = false;
               opt.Password.RequireLowercase = false;
               opt.Password.RequireDigit = true;
               opt.User.RequireUniqueEmail = true;
           }).AddEntityFrameworkStores<MyContext>();


            // services.AddAuthentication();
            //services.AddAuthorization();
            services.AddEntityFrameworkSqlServer();
            services.AddControllersWithViews();
            //services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
