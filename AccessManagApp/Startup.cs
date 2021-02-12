using AccessManagApp.Service;
using AccessManagApp.Services.Interfaces;
using AutoMapper;
using DataAccessLayer.DTOs;
using DataAccessLayer.Models;
using DataAccessLayer.Pagination;
using DataAccessLayer.Repository;
using DataAccessLayer.Repository.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Web.Http;
using TestApp.Interfaces;
using TestApp.Services;

namespace AccessManagApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //add AutoMapper
            RegisterMapperConfigruation(services);

            //register components
            RegisterComponents(services);

            services.AddDbContext<DataContext>(opt =>
                                               opt.UseInMemoryDatabase("DataList"));

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

            });
        }

     
        private static void AddTestData(DataContext context)
        {
            var testUser1 = new User
            {
                LoginName = "user1",
                Roles = new List<Role>()
                {
                    new Role()
                    {
                        Name = "a"
                    }
                }
            };

            var testUser2 = new User
            {
                LoginName = "user2",
                Roles = new List<Role>()
                {
                    new Role()
                    {
                        Name = "b"
                    }
                }
            };

            context.Users.Add(testUser1);
            context.Users.Add(testUser2);

            context.SaveChanges();
        }

        private void RegisterMapperConfigruation(IServiceCollection services)
        {
            var config = new MapperConfiguration(cfg =>
            {
                //Create all maps here
                cfg.CreateMap<User, UserDTO>();
                cfg.CreateMap<UserDTO, User>();
                cfg.CreateMap<Role, RoleDTO>();
                cfg.CreateMap<RoleDTO, Role>();
            });

            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);

        }

        private void RegisterComponents(IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped(typeof(IPaginationHandler<>), typeof(PaginationHandler<>));
        }
    }
}
