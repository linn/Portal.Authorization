namespace Linn.Portal.Authorization.IoC
{
    using Linn.Common.Persistence;
    using Linn.Common.Persistence.EntityFramework;
    using Linn.Portal.Authorization.Domain;
    using Linn.Portal.Authorization.Persistence;
    using Linn.Portal.Authorization.Persistence.Repositories;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public static class PersistenceExtensions
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            return services.AddScoped<ServiceDbContext>()
                .AddScoped<DbContext>(a => a.GetService<ServiceDbContext>())
                .AddScoped<ISubjectRepository, SubjectRepository>()
                .AddScoped<IRepository<Permission, int>, EntityFrameworkRepository<Permission, int>>(
                    r => new EntityFrameworkRepository<Permission, int>(
                        r.GetService<ServiceDbContext>()?.Permissions))
                .AddScoped<IRepository<Privilege, int>, EntityFrameworkRepository<Privilege, int>>(
                    r => new EntityFrameworkRepository<Privilege, int>(
                        r.GetService<ServiceDbContext>()?.Privileges))
                .AddScoped<IRepository<Association, int>, EntityFrameworkRepository<Association, int>>(
                    r => new EntityFrameworkRepository<Association, int>(
                        r.GetService<ServiceDbContext>()?.Associations))
                .AddScoped<ITransactionManager, TransactionManager>();
        }
    }
}
