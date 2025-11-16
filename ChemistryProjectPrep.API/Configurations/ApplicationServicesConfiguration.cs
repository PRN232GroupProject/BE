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
            services.AddScoped<IChapterDAO, ChapterDAO>();
            services.AddScoped<IResourceDAO, ResourceDAO>();

            // Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IChapterRepository, ChapterRepository>();
            services.AddScoped<IResourceRepository, ResourceRepository>();

            // Services
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IChapterService, ChapterService>();
            services.AddScoped<IResourceService, ResourceService>();

            //Customer Services


            return services;
        }
    }
}