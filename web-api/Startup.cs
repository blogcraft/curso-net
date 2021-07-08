using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using web_api.Data.AppDb.Context;
using web_api.Data.AuthDb.Context;
using web_api.Services;

namespace web_api
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
            services.AddDbContext<AppdbContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("BasedatosConnection")));

            services.AddDbContext<AuthDbContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("BaseAuthConnection")));

            services.AddScoped<IClienteService, ClienteService>();
            services.AddScoped<ICarteraService, CarteraService>();
            services.AddSingleton<ICalculoService, CalculoService>();

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AuthDbContext>();

            services.Configure<IdentityOptions>(options =>
                {
                    // Contraseñas
                    options.Password.RequireDigit = true;
                    options.Password.RequiredLength = 10;
                    options.Password.RequireNonAlphanumeric = false;

                    // Tipo Fuera
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                    options.Lockout.MaxFailedAccessAttempts = 10;

                    // Usuario
                    options.User.RequireUniqueEmail = true;
                });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "web_api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "web_api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
