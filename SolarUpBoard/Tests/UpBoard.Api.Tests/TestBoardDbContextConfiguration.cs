using Board.Infrastucture.DataAccess;
using Board.Infrastucture.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UpBoard.Infrastructure.DataAccess;

namespace Board.Api.Tests
{
    public class TestBoardDbContextConfiguration : IDbContextOptionsConfigurator<UpBoardDbContext>
    {
        public const string InMemoryDatabaseName = "SolarUpBoardDb";

        private readonly ILoggerFactory _loggerFactory;

        public TestBoardDbContextConfiguration(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }

        public void Configure(DbContextOptionsBuilder<UpBoardDbContext> options)
        {
            options.UseInMemoryDatabase(InMemoryDatabaseName);
            options.UseLoggerFactory(_loggerFactory);
            options.EnableSensitiveDataLogging();
        }
    }
}