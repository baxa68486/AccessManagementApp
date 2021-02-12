using AccessManagApp.Services.Interfaces;
using DataAccessLayer.Models;
using DataAccessLayer.Pagination;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TestApp.Interfaces
{
    public interface IUserService : IGenericService<User>
    {
        Task<ResponseModel<User>> FindAllAsync(int pageNumber, int pageSize, Expression<Func<User, bool>> predicate);
        Task<ResponseModel<User>> FindAllAsync(int pageNumber, int pageSize);
    }
}
