using DataAccessLayer.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace AccessManagApp
{
    public class DbInitializer : IDbInitializer
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public DbInitializer(IServiceScopeFactory scopeFactory)
        {
            this._scopeFactory = scopeFactory;
        }

        public void SeedData()
        {
            using (var serviceScope = _scopeFactory.CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<DataContext>())
                {
                    Role role1 = new Role()
                    {
                        Name = "manager"
                    };

                    Role role2 = new Role()
                    {
                        Name = "developer"
                    };

                    var testUser1 = new User
                    {
                        LoginName = "Baxa",
                        Email = "baxa--68486@mail.ru",
                        Roles = new List<Role>()
                        {
                            role1,
                            role2
                        }
                    };

                    var testUser2 = new User
                    {
                        LoginName = "Shemsi",
                        Email = "Shemsi--68486@mail.ru",
                        Roles = new List<Role>()
                        {
                            role1,
                            role2
                        }
                    };
                    context.Users.Add(testUser1);
                    context.Users.Add(testUser2);

                    context.SaveChanges();
                }
            }
        }
    }
}
