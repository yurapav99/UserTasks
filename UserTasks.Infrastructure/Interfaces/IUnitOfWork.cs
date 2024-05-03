using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserTasks.Infrastructure.Persistance;

namespace UserTasks.Infrastructure.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IAssignmentRepository AssignmentRepository { get; }

        IUserRepository UserRepository { get; }

        IUserAssigmentHistoryRepository AssigmentHistoryRepository { get; }

        UserAssignmentsDbContext UserAssignmentsDbContext { get; }

        Task<int> SaveAsync();
    }
}
