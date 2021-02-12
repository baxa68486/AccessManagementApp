using AccessManagApp.Services.Interfaces;
using DataAccessLayer.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AccessManagApp.Service
{
    public class GenericService<Entity> : IGenericService<Entity>
    {
        protected readonly IGenericRepository<Entity> repository;

        public GenericService(IGenericRepository<Entity> repository)
        {
            this.repository = repository;
        }
       
        public async Task<IEnumerable<Entity>> FindAllAsync()
        {
            return await repository.FindAllAsync();
        }

        public virtual async Task<Entity> FindByAsync(Expression<Func<Entity, bool>> predicate)
        {
            return await repository.FindByAsync(predicate);
        }

        public virtual async Task<Entity> FindByIdAsync(int id)
        {
            return await repository.FindByIdAsync(id);
        }

        public virtual async Task AddAsync(Entity item)
        {
            await repository.AddAsync(item);
        }

        public virtual async Task AddRangeAsync(IEnumerable<Entity> entities)
        {
             await repository.AddRangeAsync(entities);;
        }

        public virtual async Task RemoveAsync(Entity item)
        {
            await repository.RemoveAsync(item);
        }

        public virtual async Task RemoveRangeAsync(IEnumerable<Entity> entities)
        {
            await repository.RemoveRangeAsync(entities);
        }
    }
}