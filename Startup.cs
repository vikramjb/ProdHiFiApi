using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ProdHiFiApi.Data;
using ProdHiFiApi.Models;
using ProdHiFiApi.Models.Interface;

namespace ProdHiFiApi
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
            services.AddEntityFrameworkInMemoryDatabase().AddDbContext<ProductDbContext>((serviceProvider, options) =>
            {
                DbContextOptionsBuilder dbContextOptionsBuilder1 = options.UseInMemoryDatabase("Products").UseInternalServiceProvider(serviceProvider);
                var dbContextOptionsBuilder = dbContextOptionsBuilder1;
            });
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
            services.AddTransient<BaseDataSeeder>();
            services.AddAuthentication(
                config =>
                {
                    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }
            )
           .AddJwtBearer(config =>
           {
               config.RequireHttpsMetadata = false;
               config.SaveToken = true;
               config.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
               {
                   ValidIssuer = Configuration["Tokens:Issuer"],
                   ValidAudience = Configuration["Tokens:Issuer"],
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Tokens:Key"]))
               };
           });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, BaseDataSeeder dataSeeder)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
                app.UseHttpsRedirection();
            }
            app.UseMvc();
            dataSeeder.SeedAsync().Wait();
        }
    }
}
