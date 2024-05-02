using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserTasks.Infrastructure.Persistance
{
    public class UserAssignmentsDbContextFactory : IDesignTimeDbContextFactory<UserAssignmentsDbContext>
    {
        public UserAssignmentsDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<UserAssignmentsDbContext>();
            var connectionString = configuration.GetConnectionString("AppDbConnectionString");
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

            return new UserAssignmentsDbContext(optionsBuilder.Options, configuration);
        }
    }
}
