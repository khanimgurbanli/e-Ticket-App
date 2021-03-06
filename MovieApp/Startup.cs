using eTickets.Data;
using eTickets.Data.Cart;
using eTickets.Data.Context;
using eTickets.Data.Manager;
using eTickets.Data.Services;
using eTickets.Data.Static;
using eTickets.Models;
using eTickets.Models.Validators;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MovieApp
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
            services.AddDbContext<AppDBContext>(options =>
                  options.UseSqlServer(
                      Configuration.GetConnectionString("DefaultConnection")));

            //Actors Services
            services.AddScoped<IActorService, ActorManager>();
            services.AddScoped<IProducerService, ProducerManager>();
            services.AddScoped<ICinemaService, CinemaManager>();
            services.AddScoped<IMovieService, MovieManager>();
            services.AddScoped<IOrderService, OrderManager>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped(sc => ShoppingCard.GetShppingCard(sc));

            //Authentication and Authoriation
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AppDBContext>();
            services.AddMemoryCache();

            services.AddSession();

            services.AddAuthentication(option =>
            {
                option.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            });
            services.AddControllersWithViews();

            //Validation
            services.AddFluentValidation(x =>
            {
                x.DisableDataAnnotationsValidation = true;
               // For Single Class==> x.RegisterValidatorsFromAssemblyContaining<ActorValidator>();
                x.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession();


            //Authentication Authorization
            app.UseAuthentication();
            app.UseAuthorization();


            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            //Seed
            AppDbInitializier.Seed(app);
            AppDbInitializier.SeedUsersAndRolesAsync(app).Wait();
        }
    }
}
