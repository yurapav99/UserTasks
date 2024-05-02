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
    public class JobManager : IJobManager
    {
        public IUnitOfWork _unitOfWork;
        private readonly Random _random;

        public JobManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _random = new Random();
        }

        public async Task Proceed()
        {
           await ReassignedAsignments();
        }

        private async Task ReassignedAsignments()
        {
            var assignments = await _unitOfWork.AssignmentRepository.Get()
                .Where(a => a.Status != Status.Completed)
                .Include(a => a.User)
                .ToListAsync();
            var users = await _unitOfWork.UserRepository.Get().ToListAsync();

            foreach (var assignment in assignments)
            {
                // Get history for this assignment
                var history = await _unitOfWork.AssigmentHistoryRepository.Get()
                    .Where(h => h.AssigmentId == assignment.Id)
                    .ToListAsync();

                var assignedUserIds = history.Select(h => h.UserId).Distinct().ToList();
                var transferCount = history.Count;

                // Filter users to exclude the ones already assigned to this task if they need to be assigned at least once
                var eligibleUsers = users.Where(u => !assignedUserIds.Contains(u.Id)).ToList();
                if (!eligibleUsers.Any() && transferCount >= 3)
                {
                    // All users have been assigned and the task transferred minimum 3 times
                    assignment.Status = Status.Completed;
                    assignment.UserId = null;
                    assignment.User = null;
                }
                else
                {
                    if (!eligibleUsers.Any())  // If all have been assigned, restart from any user not the last assigned
                        eligibleUsers = users.Where(u => u.Id != assignment.UserId).ToList();

                    var newUser = eligibleUsers.OrderBy(u => Guid.NewGuid()).FirstOrDefault();
                    assignment.UserId = newUser.Id;
                    assignment.User = newUser;

                    // Add to history
                    _unitOfWork.AssigmentHistoryRepository.Insert(new UserAssigmentHistory
                    {
                        UserId = newUser.Id,
                        AssigmentId = assignment.Id,
                        User = newUser,
                        Assigment = assignment
                    });

                    assignment.Status = Status.InProgress; // Continue as in progress
                }
            }

            await _unitOfWork.SaveAsync();
        }
    }
}
