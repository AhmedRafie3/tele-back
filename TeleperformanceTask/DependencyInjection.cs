using Application.Interfaces;
using Application.Repository.IBase;
using Application.Services;
using Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using TeleperformanceTask.Auth;
using TeleperformanceTask.Model;

namespace TeleperformanceTask
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext(configuration);
            services.AddRepositories();
            services.AddRepo();

            return services;
        }

        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationDBContext>(options =>
               options.UseSqlServer(connectionString,
                   builder => builder.MigrationsAssembly(typeof(ApplicationDBContext).Assembly.FullName)));
        }

        private static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<DbContext, ApplicationDBContext>();
            services
                .AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork))
                .AddTransient(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));


        }
        private static void AddRepo(this IServiceCollection services)
        {
            services.AddScoped<UserService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
        }
    }
}
