using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserTasks.Infrastructure.Services.Interfaces;

namespace UserTasks.Infrastructure.Jobs
{
    public class HangfireConfiguration
    {
        private readonly IRecurringJobManager _recurringJobManager;
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IJobManager _jobManager;

        public HangfireConfiguration(IRecurringJobManager recurringJobManager, IBackgroundJobClient backgroundJobClient, IJobManager jobManager)
        {
            _recurringJobManager = recurringJobManager;
            _backgroundJobClient = backgroundJobClient;
            _jobManager = jobManager;
        }

        public async Task ConfigureAsync()
        {
                _recurringJobManager.AddOrUpdate(
                    "DataCleanupJob",
                    () => AsyncOperationJob(),
                    Cron.MinuteInterval(2));

        }

        public async Task AsyncOperationJob()
        {
            await _jobManager.Proceed(); // Execute the async method
        }


    }

}
