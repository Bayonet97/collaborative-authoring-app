using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CA.Services.AuthoringService.API.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace CA.Services.AuthoringService.API.Middleware
{
    public static class WebsocketMiddlewareExtensions
    {
        public static IApplicationBuilder UseWebsocketServer(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthoringWebsocketMiddleware>();
        }

        public static IServiceCollection AddWebSocketController(this IServiceCollection services)
        {
            services.AddSingleton<AuthoringWebsocketController>();
            return services;
        }
    }
}
