using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<IEnumerable<User>> FindAllAsync(int pageNumber, int pageSize,
                                        Expression<Func<User, bool>> predicate);
        Task<IEnumerable<User>> FindAllAsync(int pageNumber, int pageSize);
        Task<User> GetUserWithRolesAsync(string loginName);
    }
}