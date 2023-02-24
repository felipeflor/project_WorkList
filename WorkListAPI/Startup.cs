using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkListAPI.Src.Repositories.Implements;
using WorkListAPI.Src.Contexts;
using WorkListAPI.Src.Repositories;
using WorkListAPI.Src.Services;
using WorkListAPI.Src.Services.Implements;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace WorkListAPI
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
            // Database configuration
            services.AddDbContext<Context>(opt =>
            opt.UseSqlServer(Configuration["ConnectionStringsDev:DefaultConnection"]));

            //Repositories
            services.AddScoped<IUser, UserRepository>();
            services.AddScoped<IWork, WorkRepository>();

            //Controllers
            services.AddCors();
            services.AddControllers();

            //Configuration Services
            services.AddScoped<IAuthentication, AuthenticationServices>();

            // Configuração do Token Autenticação JWTBearer
            var key = Encoding.ASCII.GetBytes(Configuration["Settings:Secret"]);
            services.AddAuthentication(a =>
            {
                a.DefaultAuthenticateScheme =
                JwtBearerDefaults.AuthenticationScheme;
                a.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(b =>
            {
                b.RequireHttpsMetadata = false;
                b.SaveToken = true;
                b.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            }
            );
        }

    }
    
    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, Context context)
    {
        //Development environment
        if (env.IsDevelopment())
        {
            context.Database.EnsureCreated();
            app.UseDeveloperExceptionPage();
        }

        //Prodution environment
        context.Database.EnsureCreated();

        app.UseRouting();

        app.UseCors(c => c
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

        app.UseAuthorization();
        app.UseAuthentication();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
