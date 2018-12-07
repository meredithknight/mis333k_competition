using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fa18Team22.DAL;
using fa18Team22.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace fa18Team22
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //CONNECTION STRING FOR FINAL PROJECT TO TURN IN --> fa18team22final_project (DB)
            var connectionString = "Server=tcp:fa18team22finalprojectus.database.windows.net,1433;Initial Catalog=fa18team22FINAL_PROJECT;Persist Security Info=False;User ID=MISAdmin;Password=Password22;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            //BACKUP CONNECTION STRING FOR FINAL PROJECT TO TURN IN --> fa18team22final_project_BACKUP (DB)
            //var connectionString = "Server=tcp:fa18team22finalprojectbackup.database.windows.net,1433;Initial Catalog=fa18team22FINAL_PROJECT_BACKUP;Persist Security Info=False;User ID=MISAdmin;Password=Password22;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            //CONNECTION STRING TO MESS WITH
            //var connectionString = "Server=tcp:fa18team22v2copy.database.windows.net,1433;Initial Catalog=fa18team22_v2_Copy;Persist Security Info=False;User ID=MISAdmin;Password=Password22;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";


            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

            services.AddIdentity<AppUser, IdentityRole>(opts => {
                opts.User.RequireUniqueEmail = true;
                opts.Password.RequiredLength = 6;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireDigit = false;
            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            services.Configure<FormOptions>(options =>
            {
                options.ValueCountLimit = 5000; // 5000 items max
                options.ValueLengthLimit = 1024 * 1024 * 100; // 100MB max len form data
            });


            services.AddMvc();



            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider service, AppDbContext db)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();

            //added 11/27
            app.UseStatusCodePages();

            app.UseMvc(routes => {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "Home", action = "Index" });
            });

            //Customers must be seeded first
            //Seeding.SeedGenres.SeedAllGenres(db);
            //Seeding.SeedIdentity.AddAdmin(service).Wait();
            //Seeding.SeedBooks.SeedAllBooks(db);
            //Seeding.SeedCustomers.SeedAllCustomersAsync(db, service).Wait();
            //Seeding.SeedEmployees.SeedAllEmployeesAsync(db, service).Wait();

        }
    }
}
