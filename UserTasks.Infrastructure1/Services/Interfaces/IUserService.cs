using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserTasks.Domain.Models;

namespace UserTasks.Infrastructure.Services.Interfaces
{
    public interface  IUserService
    {
        Task<IEnumerable<User>> GetUsers();
    }
}
