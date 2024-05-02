using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserTasks.Infrastructure.Interfaces;
using UserTasks.Infrastructure.Repositories;
using UserTasks.Infrastructure;
using UserTasks.Application.Interfaces;
using UserTasks.Application.Services;

namespace UserTasks.Application.Extensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddDIServices2(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
