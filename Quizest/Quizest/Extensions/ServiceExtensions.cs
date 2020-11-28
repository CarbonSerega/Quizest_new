using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Contracts;
using Entities;
using LoggerService;
using Repository;
using Repository.MongoServices;

namespace Quizest.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services) =>
            services.AddCors(o =>
                o.AddPolicy("CorsPolicy", builder =>
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

        public static void ConfigureIISIntegration(this IServiceCollection services) =>
            services.Configure<IISOptions>(o => { });

        public static void ConfigureLoggerService(this IServiceCollection services)
            => services.AddScoped<ILoggerManager, LoggerManager>();

        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
            => services.AddDbContext<RepositoryContext>(o 
                => o.UseSqlServer(configuration.GetConnectionString("QuizestApplicationDB"), b
                    => b.MigrationsAssembly("Quizest")));

        public static void ConfigureSQLRepositoryManager(this IServiceCollection services) 
            => services.AddScoped<ISQLRepositoryManager, SQLRepositoryManager>();

        public static void ConfigureMongoConnectionService(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoDbSettings>(
                configuration.GetSection(nameof(MongoDbSettings)));

            services.AddSingleton<IMongoDbSettings>(sp =>
                sp.GetRequiredService<IOptions<MongoDbSettings>>().Value);

            services.AddSingleton<QuizService>();
        }
    }
}
