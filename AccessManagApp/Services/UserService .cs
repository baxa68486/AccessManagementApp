using System;
using System.Linq.Expressions;
using System.Linq;
using DataAccessLayer.Models;
using DataAccessLayer.Repository.Interfaces;
using DataAccessLayer.Pagination;
using TestApp.Interfaces;
using AccessManagApp.Service;
using System.Threading.Tasks;

namespace TestApp.Services
{
    public class UserService : GenericService<User>, IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IPaginationHandler<User> _paginationHandler;
        public UserService(IUserRepository repository, IPaginationHandler<User> paginationHandler) : base(repository)
        {
            _paginationHandler = paginationHandler;
            _repository = repository;
        }

        public async Task<ResponseModel<User>> FindAllAsync(int pageNumber, int pageSize,
                                               Expression<Func<User, bool>> predicate)
        {
            var res = await _repository.FindAllAsync(pageNumber, pageSize, predicate);
            return _paginationHandler.Create(res, pageNumber, pageSize, res.Count());
        }

        public async Task<ResponseModel<User>> FindAllAsync(int pageNumber, int pageSize)
        {
            var res = await _repository.FindAllAsync(pageNumber, pageSize);
            return _paginationHandler.Create(res, pageNumber, pageSize, res.Count());
        }
    }
}