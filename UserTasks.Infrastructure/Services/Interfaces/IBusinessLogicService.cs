using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserTasks.Infrastructure.Services.Interfaces
{
    public interface IBusinessLogicService
    {
        public Task Proceed();
    }
}
