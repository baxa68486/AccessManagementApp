using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository.Interfaces
{
    public interface IGenericRepository<Entity>
    {
        Task<Entity> FindByAsync(Expression<Func<Entity, bool>> predicate);
        Task<IEnumerable<Entity>> FindAllAsync();
        Task<Entity> FindByIdAsync(int id);
        Task RemoveAsync(Entity entity);
        Task RemoveRangeAsync(IEnumerable<Entity> entities);
        Task AddAsync(Entity entity);
        Task AddRangeAsync(IEnumerable<Entity> entities);
        void SetEntityState<T>(T entity, EntityState entityState) where T : class;
        int Count();
    }
}