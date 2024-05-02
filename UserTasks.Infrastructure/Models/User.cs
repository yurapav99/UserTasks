using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserTasks.Domain.Models;

public class User : Entity
{
    public string Name { get; set; } = null!;

    public ICollection<Assignment>? Assignments { get; set; }

    public ICollection<UserAssigmentHistory>? UserAssigmentHistories { get; set; }

}

