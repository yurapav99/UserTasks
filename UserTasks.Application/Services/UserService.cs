using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserTasks.Application.Interfaces;
using UserTasks.Domain.Models;
using UserTasks.Infrastructure;
using UserTasks.Infrastructure.Interfaces;

namespace UserTasks.Application.Services
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
            return await _unitOfWork.UserRepository.GetAll();
        
        }
    }
}
