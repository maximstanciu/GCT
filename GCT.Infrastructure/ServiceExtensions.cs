using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using GCT.Migrations;
using GCT.Contracts.Data;
using GCT.Infrastructure.Data;
using GCT.Core.StateMachine;
using GCT.Infrastructure.StateMachine;

namespace GCT.Infrastructure
{
    public static class ServiceExtensions
    {
        private static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            return services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        private static IServiceCollection AddDatabaseContext(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddSqlite<DatabaseContext>(configuration.GetConnectionString("DefaultConnection"), (options) =>
            {
                options.MigrationsAssembly("GCT.Migrations");
            });
        }

        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddDatabaseContext(configuration).AddUnitOfWork();
        }

        public static IServiceCollection AddFSM(this IServiceCollection services)
        {
            return services.AddScoped<IProcessSM, ProcessSM>();
        }
    }
}
