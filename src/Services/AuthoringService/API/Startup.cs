using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using CA.Services.AuthoringService.API.Middleware;
using CA.Services.AuthoringService.API.Controllers;
using CA.Services.AuthoringService.Domain.AggregatesModel.BookAggregate;
using CA.Services.AuthoringService.Infrastructure.Repositories;
using MediatR;
using System.Reflection;

namespace CA.Services.AuthoringService.API
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AuthoringApi", Version = "v1" });
            });
            services.AddMediatR(Assembly.GetAssembly(typeof(Startup)));
            services.AddSingleton<IBookRepository, BookRepository>();
            //services.AddWebSocketController();
            services.AddCors();
            services.AddSignalR();
            services.AddHttpContextAccessor();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AuthoringApi"));
            }
            
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseCors(builder => builder
            .WithOrigins("null")
            .AllowAnyHeader()
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
            );

            app.UseHttpsRedirection();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<AuthoringHub>("/api/authoring/live");
                endpoints.MapControllers();
            });
        }
    }
}
