using BusinessObjects.DAO.Base.Implements;
using BusinessObjects.DAO.Implements;
using BusinessObjects.DAO.Interfaces;
using BusinessObjects.Mapper;
using Repository.Implements;
using Repository.Interfaces;
using Service.Implements;
using Service.Interfaces;

namespace ChemistryProjectPrep.API.Configurations
{
    public static class ApplicationServicesConfiguration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<TokenProvider>();

            // Mapper
            services.AddScoped<IMapperlyMapper, MapperlyMapper>();

            // DAOs
            services.AddScoped<IUserDAO, UserDAO>();

            // Repositories
            services.AddScoped<IUserRepository, UserRepository>();

            // Services
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}