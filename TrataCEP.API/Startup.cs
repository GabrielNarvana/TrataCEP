using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TrataCEP.API.Controllers;
using TrataCEP.API.Data.Repositories;
using TrataCEP.API.Data.Repositories.Interfaces;
using TrataCEP.API.Helpers;
using TrataCEP.API.Infra.Factory;
using TrataCEP.API.Infra.Interface;

namespace TrataCEP.API
{
    public class Startup
    {
        IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient();


            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile($"appsettings.json");
            _configuration = builder.Build();

            var serviceProvider = services.BuildServiceProvider();


            services.AddControllers();
            services.AddSingleton<IConfiguration>(_configuration);
            services.AddTransient<IPostgressConnection>(s => new PostgressConnectionFactory(_configuration.GetSection("postgresConnection").Value));
            services.AddSingleton<IEnderecoRepository, EnderecoRepository>();

    
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            //app.UseRouting();

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.Run(async (context) =>
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                await context.Response.WriteAsync($"Aplication {assembly.GetName()}");
            });
        }
    }
}
