using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleChaves.Application.Database;
using ControleChaves.Application.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutoMapper;
using ControleChaves.Application.AutoMapper;
using ControleChaves.Application.Entities;
using Microsoft.AspNetCore.Http;

namespace ControleChaves
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
            services.AddControllersWithViews();

            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();

            services.RegisterMappings();

            services.AddDbContext<ControleChavesContext>();

            services.AddSingleton(Configuration);
            services.AddTransient<IUserService, UserService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IFuncionarioService, FuncionarioService>();
            services.AddScoped<ILocalizacaoService, LocalizacaoService>();
            services.AddScoped<IChaveService, ChaveService>();
            services.AddScoped<IControleService, ControleService>();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
                options.LoginPath = "/auth/login";
                options.LogoutPath = "/auth/logout";
                options.SlidingExpiration = true;
            });
        }

        private static void UpdateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<ControleChavesContext>())
                {
                    context.Database.EnsureCreated();

                    if (context.Usuarios.Count() == 0)
                    {
                        context.Usuarios.Add(new Usuario() 
                        {
                            Email = "root@root.com",
                            Nome = "Administrador",
                            Senha = "123456",
                            Status = Status.ATIVO,
                        });

                        context.SaveChanges();
                    }

                    context.Seed();
                }
            }
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

            UpdateDatabase(app);

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
