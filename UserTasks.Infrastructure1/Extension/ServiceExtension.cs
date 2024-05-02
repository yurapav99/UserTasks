using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserTasks.Infrastructure.Interfaces;
using UserTasks.Infrastructure.Repositories;
using UserTasks.Infrastructure.Services;
using UserTasks.Infrastructure.Services.Interfaces;

namespace UserTasks.Infrastructure.Extension
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddDIServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAssignmentRepository, AssignmentRepository>();
            services.AddScoped<IUserAssigmentHistoryRepository, UserAssigmentHistoryRepository>();

            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IJobManager, JobManager>();


            return services;
        }
    }
}
