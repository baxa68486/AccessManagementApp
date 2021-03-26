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
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly DataContext _dbContext;
        public UserRepository(DataContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<User>> FindAllAsync(int pageNumber, int pageSize, Expression<Func<User, bool>> predicate)
        {
            Expression<Func<User, string>> orderByName = order => order.LoginName;
            var query = dbSet.Include(u => u.Roles)
                             .Where(predicate)
                             .OrderBy(orderByName)
                             .Skip((pageNumber - 1) * pageSize)
                             .Take(pageSize);
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<User>> FindAllAsync(int pageNumber, int pageSize)
        {
            Expression<Func<User, string>> orderByName = order => order.LoginName;
            var res  = await  dbSet.Include(u => u.Roles)
                                    .OrderBy(orderByName)
                                    .Skip((pageNumber - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToListAsync();
            return  res;
        }

        public override async Task<IEnumerable<User>> FindAllAsync()
        {
            var res = await dbSet.Include(u => u.Roles)
                                 .ToListAsync();
            return res;
        }

        public override async Task<User> FindByAsync(Expression<Func<User, bool>> predicate)
        {
              var ls =  await dbSet.Where(predicate)
                              .Include(user => user.Roles)
                              .ToListAsync();
              return ls.FirstOrDefault();
        }

        public override async Task AddAsync(User user)
        {
            await _dbContext.Users.AddAsync(user);
            if (user.Roles != null)
            {
                foreach (var role in user.Roles)
                {
                    _dbContext.Roles.Attach(role);
                }
            }
            await _dbContext.SaveChangesAsync();
        }

        public async Task<User> GetUserWithRolesAsync(string loginName)
        {
            var q = dbSet.Where((us) => us.LoginName.Equals(loginName))
                         .Include(us => us.Roles);
            var ls = await q.ToListAsync();

            return ls.FirstOrDefault();        
        }

    }
}