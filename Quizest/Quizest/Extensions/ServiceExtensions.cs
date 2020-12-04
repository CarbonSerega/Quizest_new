using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;
using Contracts;
using Entities;
using LoggerService;
using Repository;
using Repository.MongoServices;
using Contracts.Repos.Mongo;

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
                => o.UseLazyLoadingProxies()
                    .UseSqlServer(configuration.GetConnectionString("QuizestApplicationDB"), b
                    => b.MigrationsAssembly("Quizest")));

        public static void ConfigureSQLRepositoryManager(this IServiceCollection services) 
            => services.AddScoped<ISQLRepositoryManager, SQLRepositoryManager>();

        public static void ConfigureMongoConnectionService(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoDbSettings>(
                configuration.GetSection(nameof(MongoDbSettings)));

            services.AddSingleton<IMongoDbSettings>(sp =>
                sp.GetRequiredService<IOptions<MongoDbSettings>>().Value);

            services.AddScoped<IMongoService, QuizService>();
        }

        public static void ConfigureApiBehavior(this IServiceCollection services) =>
            services.Configure<ApiBehaviorOptions>(o =>
            {
                o.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState
                    .Where(e => e.Value.Errors.Count > 0)
                    .Select(e => e.Value.Errors)
                    .Select(e => e[0].ErrorMessage);

                    var errorObject = new
                    {
                        message = "Validation Error!",
                        details = errors
                    };

                    return new BadRequestObjectResult(errorObject);
                };
            });

    }
}
