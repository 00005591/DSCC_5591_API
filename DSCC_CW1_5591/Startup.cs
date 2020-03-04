using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using DSCC_CW1_5591.Models;
using DSCC_CW1_5591.Repositories;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.OpenApi.Models;

namespace DSCC_CW1_5591
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
            services.AddMvc();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Products API",
                    Description = "This is a simple Grocery store server which is created in order to fulfill DSCC coursework requirements.",
                    TermsOfService = new Uri("https://lebazar.uz/agreement"),
                    Contact = new OpenApiContact
                    {
                        Name = "00005591",
                        Email = "a00005591@gmail.com",
                    },
                });

                var xmlPath = System.AppDomain.CurrentDomain.BaseDirectory + @"DSCC_CW1_5591.xml";
                c.IncludeXmlComments(xmlPath);
            });
            var connection = Configuration.GetConnectionString("RDSConnection");
            services.AddControllers();
            services.AddDbContext<DataBaseContext>(options => options.UseSqlServer(connection));

            services.AddTransient<IProductRepository, ProductRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI( c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Products API");
                } );
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
