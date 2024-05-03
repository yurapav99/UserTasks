using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserTasks.Infrastructure.Services;
using UserTasks.Infrastructure.Services.Interfaces;

namespace UserTasks.Infrastructure.Jobs
{
    public class HangfireConfiguration
    {
        private readonly IRecurringJobManager _recurringJobManager;
        private readonly IBusinessLogicService _businessLogicService;

        private readonly string JobName = "UserAssignmentsJob";
        private readonly int TimeIntervalMinutes = 2;

        public HangfireConfiguration(IRecurringJobManager recurringJobManager, IBusinessLogicService businessLogicService)
        {
            _recurringJobManager = recurringJobManager;
            _businessLogicService = businessLogicService;
        }

        public async Task ConfigureAsync()
        {
                _recurringJobManager.AddOrUpdate(
                    JobName,
                    () => AsyncOperationJob(),
                    Cron.MinuteInterval(TimeIntervalMinutes));
        }

        public async Task AsyncOperationJob()
        {
            await _businessLogicService.Proceed(); // Execute the async method
        }
    }

}
