using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjMongoDBUser.Data;
using ProjMongoDBUser.Util;
using Microsoft.Extensions.Options;
using Models.Model;
using ProjMongoDBUser.Service;
using ProjMongoDBUser.Properties;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ProjMongoDBAPIJWT.Config;

namespace ProjMongoDBUser
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
            services.AddCors();
            
            services.AddControllers();

            var key = Encoding.ASCII.GetBytes(SettingsJWT.Secret);              

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProjMongoDBUser", Version = "v1" });
            });

            /*services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters =   new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.Configure<ProjMongoDotnetDatabaseSettings>(
                Configuration.GetSection(nameof(ProjMongoDotnetDatabaseSettings)));

            services.AddSingleton<IProjMongoDotnetDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<ProjMongoDotnetDatabaseSettings>>().Value);

            services.AddSingleton<UserService>();*/
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
                if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProjMongoDBUser v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            /*
            app.UseCors(x => x                                                                            
              .AllowAnyOrigin()                                                                           
              .AllowAnyMethod()                                                                           
              .AllowAnyHeader());                                                                         

            app.UseAuthentication();                                                                      
            */
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
