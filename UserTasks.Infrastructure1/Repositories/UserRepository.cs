using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserTasks.Domain.Models;
using UserTasks.Infrastructure.Interfaces;
using UserTasks.Infrastructure.Persistance;

namespace UserTasks.Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(UserAssignmentsDbContext dbContext) : base(dbContext)
        {

        }
    }
}
