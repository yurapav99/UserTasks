using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserTasks.Infrastructure.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        DbSet<T> DbSet();

        IQueryable<T> Get();

        void Insert(T entity);

        void Update(T entity);

        void Delete(T entity);

        void ClearAll();

        void AddRange(IEnumerable<T> entities);

        Task<int> SaveChangesAsync();

    }
}
