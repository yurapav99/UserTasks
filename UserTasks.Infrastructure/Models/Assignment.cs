using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserTasks.Domain.Models;

public class Assignment : Entity
{
    public string Name { get; set; } = null!;

    public Status Status { get; set; }

    public Guid? UserId { get; set; }

    public User? User { get; set; }

    public ICollection<UserAssigmentHistory>? UserAssigmentHistories { get; set; }
}

