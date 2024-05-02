using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserTasks.Domain.Models;
using UserTasks.Infrastructure.Interfaces;
using UserTasks.Infrastructure.Persistance;

namespace UserTasks.Infrastructure
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly UserAssignmentsDbContext _context;
        private DbSet<T> _dbSet;

        public GenericRepository(UserAssignmentsDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public DbSet<T> DbSet()
        {
            return _dbSet;
        }

        public IQueryable<T> Get()
        {
            return _dbSet.AsNoTracking();  // Return IQueryable for query composition
        }

        public void Insert(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _dbSet.Remove(entity);
        }

        public void ClearAll()
        {
            // Remove all entries from the set
            foreach (var entity in _dbSet)
                _dbSet.Remove(entity);

            _context.SaveChanges();
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _dbSet.AddRange(entities);
            _context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
