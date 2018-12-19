using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using projetfinalFJO.Models.Authentification;
using projetfinalFJO.Appdata;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace projetfinalFJO
{
    public class Startup
    {
        private ActualisationContext contexteActu;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            this.contexteActu = new ActualisationContext(configuration.GetConnectionString("DefaultConnection"));
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            //session services
            services.AddDistributedMemoryCache();
            services.AddSession();
            
            //ajouter le service d'authentification Identity
            services.AddIdentity<LoginUser, LoginRole>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 2;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.SignIn.RequireConfirmedEmail = false;

            }).AddEntityFrameworkStores<LoginDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            // ajouter le service EntityFramework
            services.AddDbContext<LoginDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("LoginConnection")));

            services.AddDbContext<ActualisationContext>(options =>
               options.UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection")));

            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();


            //https://forums.asp.net/t/2142697.aspx?asp+net+core+session+timeout
            //gestion du temps du cookie de connection
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
               .AddCookie(options =>
               {
                   options.Cookie.Expiration = TimeSpan.FromSeconds(30);
               });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            app.UseSession();
            app.UseMvc();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Login}/{action=Login}/{id?}");
            });

            CreateRoles(serviceProvider).Wait();
        }

        //https://www.c-sharpcorner.com/article/role-base-authorization-in-asp-net-core-2-1/
        //creation des roles et des utilisateurs au demarrage
        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            //initializing custom roles   

            var RoleManager = serviceProvider.GetRequiredService<RoleManager<LoginRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<LoginUser>>();
            string[] roleNames = { "Admin", "Sous_Commite", "Commite_Programme" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    //create the roles and seed them to the database: Question 1  
                    roleResult = await RoleManager.CreateAsync(new LoginRole() { Name=roleName,});
                }
            }

            LoginUser user = await UserManager.FindByEmailAsync("Admin@gmail.com");

            if (user == null)
            {
                user = new LoginUser()
                {
                    UserName = "Admin@gmail.com",
                    Email = "Admin@gmail.com",
                };
                var CreateUser = await UserManager.CreateAsync(user, "Password1");
                if (CreateUser.Succeeded)
                {
                    await UserManager.AddToRoleAsync(user, "Admin");
                    Utilisateur util = new Utilisateur() { Nom = "Admin@gmail.com", Prenom = "Admin@gmail.com", RegisterDate = DateTime.Now, AdresseCourriel = "Admin@gmail.com" };
                    contexteActu.Utilisateur.Add(util);
                    await contexteActu.SaveChangesAsync();
                }              
            }

            LoginUser user1 = await UserManager.FindByEmailAsync("Sous_Commite@gmail.com");

            if (user1 == null)
            {
                user1 = new LoginUser()
                {
                    UserName = "Sous_Commite@gmail.com",
                    Email = "Sous_Commite@gmail.com",
                };
                var CreateUser=await UserManager.CreateAsync(user1, "bullshit1234");
                if(CreateUser.Succeeded)
                {
                    await UserManager.AddToRoleAsync(user1, "Sous_Commite");
                    Utilisateur util = new Utilisateur() { Nom = "Sous_Commite@gmail.com", Prenom = "Sous_Commite@gmail.com", RegisterDate = DateTime.Now, AdresseCourriel = "Sous_Commite@gmail.com" };
                    contexteActu.Utilisateur.Add(util);
                    await contexteActu.SaveChangesAsync();
                }
                
            }


            LoginUser user2 = await UserManager.FindByEmailAsync("Commite_Programme@gmail.com");

            if (user2 == null)
            {
                user2 = new LoginUser()
                {
                    UserName = "Commite_Programme@gmail.com",
                    Email = "Commite_Programme@gmail.com",
                };
                var CreateUser = await UserManager.CreateAsync(user2, "bullshit1234");
                if(CreateUser.Succeeded)
                {
                    await UserManager.AddToRoleAsync(user2, "Commite_Programme");
                    Utilisateur util = new Utilisateur() { Nom = "Commite_Programme@gmail.com", Prenom = "Commite_Programme@gmail.com", RegisterDate = DateTime.Now, AdresseCourriel = "Commite_Programme@gmail.com" };
                    contexteActu.Utilisateur.Add(util);
                    await contexteActu.SaveChangesAsync();
                }
                
            }
            

        }
    }
}
