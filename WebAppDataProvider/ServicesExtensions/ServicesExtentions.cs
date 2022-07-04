using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using WebAppSqlServerDataProvider.Data;

namespace WebAppDataProvider
{
    public static class ServicesExtentions
    {
        public static void AddDataProviders(this IServiceCollection services,
                IConfiguration configuration,
                string connectionStringKey = "FStoreDbSqlConnection") {

            // Guard
            //Guard.IsNullOrEmpty(connectionString, $"Connection string {connectionStringKey} is not set.");
            var connectionString = configuration.GetConnectionString(connectionStringKey);
            if (string.IsNullOrEmpty(connectionString)) {
                throw new ArgumentNullException($"Connection string {connectionStringKey} is not set.");
            }

            //
            var options = new DbContextOptions<FStoreDBContext>();
            var builder = new DbContextOptionsBuilder<FStoreDBContext>(options);
            builder.UseSqlServer(connectionString);
            builder.EnableSensitiveDataLogging();

            services.AddPooledDbContextFactory<FStoreDBContext>(options => {
                options.UseSqlServer(connectionString, sqlServerOptionsAction => {
                    sqlServerOptionsAction.EnableRetryOnFailure();
                });
                options.EnableSensitiveDataLogging();
            });
            RegisterDataProvider(services);
        }
        #region [ Methods - Private ]
        private static void RegisterDataProvider(IServiceCollection services) {

            services.AddScoped<ICategoryDataProvider, CategoryDataProvider>();
            services.AddScoped<IMemberDataProvider, MemberDataProvider>();
            services.AddScoped<IOrderDetailDataProvider, OrderDetailDataProvider>();
            services.AddScoped<IOrderDataProvider, OrderDataProvider>();
            services.AddScoped<IProductDataProvider, ProductDataProvider>();
            services.AddDbContext<FStoreDBContext>();
        }
        #endregion
    }
}
