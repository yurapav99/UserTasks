using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserTasks.Domain.Models;
using UserTasks.Infrastructure.Interfaces;
using UserTasks.Infrastructure.Services.Interfaces;

namespace UserTasks.Infrastructure.Services
{
    public class UserService : IUserService
    {
        public IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = await _unitOfWork.UserRepository.Get().Include(u => u.Assignments).ToListAsync();

            return users;
        }
    }
}
