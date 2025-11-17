using BusinessObjects.DAO.Base.Implements;
using BusinessObjects.DAO.Implements;
using BusinessObjects.DAO.Interfaces;
using BusinessObjects.Mapper;
using Microsoft.Extensions.Options;
using Repository.Implements;
using Repository.Interfaces;
using Service.Config;
using Service.Implements;
using Service.Interfaces;

namespace ChemistryProjectPrep.API.Configurations
{
    public static class ApplicationServicesConfiguration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<TokenProvider>();

            // Mapper
            services.AddScoped<IMapperlyMapper, MapperlyMapper>();

            // DAOs
            services.AddScoped<IUserDAO, UserDAO>();
            services.AddScoped<IChapterDAO, ChapterDAO>();
            services.AddScoped<ILessonDAO, LessonDAO>();
            services.AddScoped<IResourceDAO, ResourceDAO>();
            services.AddScoped<IQuestionDAO, QuestionDAO>();
            services.AddScoped<IStudentTestSessionDAO, StudentTestSessionDAO>();
            services.AddScoped<ITestDAO, TestDAO>();

            // Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IChapterRepository, ChapterRepository>();
            services.AddScoped<ILessonRepository, LessonRepository>();
            services.AddScoped<IResourceRepository, ResourceRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<IStudentTestSessionRepository, StudentTestSessionRepository>();
            services.AddScoped<ITestRepository, TestRepository>();

            // Services
            services.AddScoped<AuthService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IChapterService, ChapterService>();
            services.AddScoped<ILessonService, LessonService>();
            services.AddScoped<IResourceService, ResourceService>();
            services.AddScoped<IQuestionService, QuestionService>();  
            services.AddScoped<ITestSessionService, TestSessionService>();
            //Customer Services

            // Custom Services
            services.AddScoped<ICloudinaryService, CloudinaryService>();

            // Third-Party Services
            RegisterThirdPartyServices(services, configuration);

            return services;
        }

        private static void RegisterThirdPartyServices(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CloudinarySetting>(options =>
            {
                options.CloudinaryUrl = configuration["Cloudinary:CloudinaryUrl"];
            });
            CloudinarySetting.Instance = services.BuildServiceProvider().GetService<IOptions<CloudinarySetting>>().Value;
        }
    }
}