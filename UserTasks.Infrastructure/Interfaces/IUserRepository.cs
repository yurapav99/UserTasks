using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserTasks.Domain.Models;

namespace UserTasks.Infrastructure.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
    }
}
