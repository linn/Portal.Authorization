namespace Linn.Portal.Authorization.IoC
{
    using Linn.Common.Persistence;
    using Linn.Common.Persistence.EntityFramework;
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
                .AddScoped<ITransactionManager, TransactionManager>();
        }
    }
}
