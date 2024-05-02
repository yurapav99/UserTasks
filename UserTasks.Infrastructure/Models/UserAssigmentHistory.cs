using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserTasks.Domain.Models
{
    public class UserAssigmentHistory : Entity
    {
        public Guid UserId { get; set; }

        public Guid AssigmentId { get; set; }

        public User User { get; set; } = null!;

        public Assignment Assigment { get; set; } = null!;
    }
}
