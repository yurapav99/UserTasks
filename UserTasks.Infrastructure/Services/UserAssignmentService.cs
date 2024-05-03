using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserTasks.Domain.Models;
using UserTasks.Infrastructure.Interfaces;
using UserTasks.Infrastructure.Repositories;
using UserTasks.Infrastructure.Services.Interfaces;

namespace UserTasks.Infrastructure.Services
{
    public class UserAssignmentService : IUserAssignmentService
    {
        public IUnitOfWork _unitOfWork;

        public UserAssignmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = await _unitOfWork.UserRepository.Get().Include(u => u.Assignments).ToListAsync();

            return users;
        }

        public async Task<User> AddUser(string name)
        {
            var user = new User();
            user.Name = name;

            _unitOfWork.UserRepository.Insert(user);

            await _unitOfWork.SaveAsync();

            return user;
        }

        public async Task<Assignment> AddAssignment(string name)
        {
            var newTask = new Assignment
            {
                Name = name,
                Status = Status.Waiting
            };

            var availableUser = await _unitOfWork.UserRepository.Get()
                .Include(ar => ar.Assignments)
                .FirstOrDefaultAsync(u => u.Assignments == null || u.Assignments.Count == 0);

            if (availableUser != null)
            {
                newTask.UserId = availableUser.Id;
                newTask.Status = Status.InProgress; 
            }

            _unitOfWork.AssignmentRepository.Insert(newTask);
            await _unitOfWork.AssignmentRepository.SaveChangesAsync();

            return newTask;
        }
    }

}

