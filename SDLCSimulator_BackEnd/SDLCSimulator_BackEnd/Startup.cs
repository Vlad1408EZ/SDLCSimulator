using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Globalization;
using FluentValidation;
using FluentValidation.AspNetCore;
using Newtonsoft.Json.Converters;
using SDLCSimulator_BackEnd.Extensions;

namespace SDLCSimulator_BackEnd
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
            ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("uk-UA");
            services.AddControllers().AddFluentValidation().AddNewtonsoftJson(opts =>
            {
                opts.SerializerSettings.Converters.Add(new StringEnumConverter());
            }); ;
            services.AddAppCors();
            services.AddAppDbContext(Configuration);
            services.AddAppRepositories();
            services.AddAppServices(Configuration);
            services.AddAppValidators();
            services.AddMyJwtBearer(Configuration);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SDLCSimulator_BackEnd", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SDLCSimulator_BackEnd v1"));
            }

            app.UseCors("MyAllowSpecificOrigins");

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
