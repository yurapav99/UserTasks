using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserTasks.Domain.Models;

namespace UserTasks.Infrastructure.Persistance
{
    public static class SeedData
    {
        public static (List<User> Users, List<Assignment> Assignments) Get()
        {
            List<User> users = new List<User>
        {
            new User { Name = "Alice" },
            new User { Name = "Bob" },
            new User { Name = "Jon" }
        };

            List<Assignment> assignments = new List<Assignment>
        {
            new Assignment { Name = "Create project on C", Status =  Status.InProgress, UserId = users.ElementAt(0).Id },
            new Assignment { Name = "Create project on Ruby", Status =  Status.InProgress, UserId = users.ElementAt(0).Id },
            new Assignment { Name = "Create project on C#", Status =  Status.InProgress, UserId = users.ElementAt(1).Id},
            new Assignment { Name = "Create project on F#", Status =  Status.InProgress, UserId = users.ElementAt(2).Id},
            new Assignment { Name = "Create project on Js", Status = Status.Waiting},
            new Assignment { Name = "Create project on Assembler", Status = Status.Completed}
        };

            return (users, assignments);
        }


    }
}
