using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserTasks.Domain.Models
{
    public class Entity
    {
        [Key]
        public Guid Id { get; set; }

        public Entity()
        {
            Id = new Guid();
        }
    }
}
