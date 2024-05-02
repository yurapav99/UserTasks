using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserTasks.Infrastructure.Interfaces;
using UserTasks.Infrastructure.Persistance;

namespace UserTasks.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly UserAssignmentsDbContext _dbContext;
        public IUserRepository UserRepository { get; }
        public IAssignmentRepository AssignmentRepository { get; }

        public IUserAssigmentHistoryRepository AssigmentHistoryRepository { get; }

        public UnitOfWork(UserAssignmentsDbContext dbContext,
                            IUserRepository userRepository, IAssignmentRepository assignmentRepository, IUserAssigmentHistoryRepository assigmentHistoryRepository)
        {
            _dbContext = dbContext;
            UserRepository = userRepository;
            AssignmentRepository = assignmentRepository;
            AssigmentHistoryRepository = assigmentHistoryRepository;
        }

        public UserAssignmentsDbContext UserAssignmentsDbContext => _dbContext;

        public async Task<int> SaveAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
        }

    }
}
