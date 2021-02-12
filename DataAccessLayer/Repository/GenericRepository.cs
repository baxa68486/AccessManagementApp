
using DataAccessLayer.Models;
using DataAccessLayer.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly DataContext dbContext;
        public readonly DbSet<T> dbSet;
        public GenericRepository(DataContext dbContext)
        {
            this.dbContext = dbContext;
            dbSet = dbContext.Set<T>();
        }

        public GenericRepository()
        {

        }

        public int Count()
        {
            return dbSet.Count();
        }

        public virtual async Task<IEnumerable<T>> FindAllAsync()
        {
            return dbSet.ToList();
        }

        public virtual async Task<T> FindByIdAsync(int id)
        {
            return dbSet.Find(id); ;
        }

        public virtual async Task<T> FindByAsync(Expression<Func<T, bool>> predicate)
        {
            var ls = await dbSet.Where(predicate).ToListAsync();
            return ls.FirstOrDefault();
        }

        public virtual async Task RemoveAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            dbSet.Remove(entity);
            await dbContext.SaveChangesAsync();
        }

        public virtual async Task RemoveRangeAsync(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public virtual async Task AddAsync(T entity)
        {
            if (entity == null) 
                throw new ArgumentNullException("entity");
             await dbSet.AddAsync(entity);
             await dbContext.SaveChangesAsync();
        }

        public virtual async Task AddRangeAsync(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public virtual void SetEntityState<T>(T entity, EntityState entityState) where T : class
        {
            var state = EntityState.Unchanged;
            switch (entityState)
            {
                case EntityState.Added:
                    state = EntityState.Added;
                    break;
                case EntityState.Deleted:
                    state = EntityState.Deleted;
                    break;
                case EntityState.Detached:
                    state = EntityState.Detached;
                    break;
                case EntityState.Modified:
                    state = EntityState.Modified;
                    break;
                default:
                    break;

            }
            dbContext.Entry(entity).State = state;
        }
    }
}