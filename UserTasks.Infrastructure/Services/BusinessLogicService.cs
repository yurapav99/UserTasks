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
    public class BusinessLogicService : IBusinessLogicService
    {
        public IUnitOfWork _unitOfWork;

        public BusinessLogicService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Proceed()
        {
           await ReassignedAsignments();
        }

        private async Task ReassignedAsignments()
        {

            using (var transaction = await _unitOfWork.UserAssignmentsDbContext.Database.BeginTransactionAsync())
            {

                var assignments = await _unitOfWork.AssignmentRepository.Get()
                .Include(a => a.User)
                .AsNoTracking()
                .ToListAsync();

                var users = await _unitOfWork.UserRepository.Get().AsNoTracking().ToListAsync();

                var histories = await _unitOfWork.AssigmentHistoryRepository.Get()
                     .AsNoTracking()
                     .ToListAsync();

                _unitOfWork.AssigmentHistoryRepository.ClearAll();
                _unitOfWork.AssignmentRepository.ClearAll();
                _unitOfWork.UserRepository.ClearAll();
               

                await _unitOfWork.SaveAsync();

                foreach (var assignment in assignments.Where(a => a.Status != Status.Completed))
                {
                    var history = histories
                        .Where(h => h.AssigmentId == assignment.Id);

                    var assignedUserIds = history.Select(h => h.UserId).Distinct().ToList();
                    var transferCount = history.Count();

                    var eligibleUsers = users.Where(u => !assignedUserIds.Contains(u.Id)).ToList();

                    var attempts = 3;

                    if (!eligibleUsers.Any() && transferCount >= attempts)
                    {
                        assignment.Status = Status.Completed;
                        assignment.UserId = null;
                        assignment.User = null;
                    }
                    else
                    {
                        if (!eligibleUsers.Any())
                            eligibleUsers = users.Where(u => u.Id != assignment.UserId).ToList();

                        var newUser = eligibleUsers.OrderBy(u => Guid.NewGuid()).FirstOrDefault();
                        assignment.UserId = newUser!.Id;

                        histories.Add(new UserAssigmentHistory
                        {
                            UserId = newUser.Id,
                            AssigmentId = assignment.Id,
                            User = newUser,
                            Assigment = assignment
                        });

                        assignment.Status = Status.InProgress;
                    }
                }

                assignments.ForEach(assignment => assignment.User = null);
                histories.ForEach(h => { h.User = null; h.Assigment = null; });
            
                var res = histories
                        .Select(m => new { m.UserId, m.AssigmentId })
                        .Distinct()
                        .ToList();


                _unitOfWork.UserRepository.AddRange(users);
                _unitOfWork.AssignmentRepository.AddRange(assignments);
                _unitOfWork.AssigmentHistoryRepository.AddRange(histories);

                await _unitOfWork.SaveAsync();
                await _unitOfWork.UserAssignmentsDbContext.Database.CommitTransactionAsync();
            }
        }
    }
}
