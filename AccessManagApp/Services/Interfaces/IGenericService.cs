using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AccessManagApp.Services.Interfaces
{
    public interface IGenericService<Entity>
    {
        Task<Entity> FindByAsync(Expression<Func<Entity, bool>> predicate);
        Task<IEnumerable<Entity>> FindAllAsync();
        Task<Entity> FindByIdAsync(int id);
        Task RemoveAsync(Entity entity);
        Task RemoveRangeAsync(IEnumerable<Entity> entities);
        Task AddAsync(Entity entity);
        Task AddRangeAsync(IEnumerable<Entity> entities);
    }
}
