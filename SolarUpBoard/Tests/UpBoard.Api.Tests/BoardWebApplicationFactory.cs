using System.Linq;
using Board.Infrastucture.DataAccess;
using Board.Infrastucture.DataAccess.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using UpBoard.Infrastructure.DataAccess;

namespace Board.Api.Tests
{
    /// <summary>
    /// Фабрика тестого варианта сервиса.
    /// </summary>
    public class BoardWebApplicationFactory : WebApplicationFactory<UpBoard.Host.Api.Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor =
                    services.SingleOrDefault(d => d.ServiceType == typeof(IDbContextOptionsConfigurator<UpBoardDbContext>));

                services.Remove(descriptor!);

                services.AddSingleton<IDbContextOptionsConfigurator<UpBoardDbContext>, TestBoardDbContextConfiguration>();
                
                var sp = services.BuildServiceProvider();
                using var scope = sp.CreateScope();
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<UpBoardDbContext>();

                db.Database.EnsureCreated();
                DataSeedHelper.InitializeDbForTests(db);
            });
        }

        /// <summary>
        /// Создать контекст тестовой БД.
        /// </summary>
        /// <returns></returns>
        public UpBoardDbContext CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<UpBoardDbContext>();
            optionsBuilder.UseInMemoryDatabase(TestBoardDbContextConfiguration.InMemoryDatabaseName);
            optionsBuilder.EnableSensitiveDataLogging();
            var dbContext = new UpBoardDbContext(optionsBuilder.Options);
            return dbContext;
        }
    }
}