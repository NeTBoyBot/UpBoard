using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpBoard.Infrastructure.DataAccess;

namespace Board.Host.DbMigrator
{
    /// <summary>
    /// Контест БД для мигратора.
    /// </summary>
    public class MigrationDbContext : UpBoardDbContext
    {
        public MigrationDbContext(Microsoft.EntityFrameworkCore.DbContextOptions options) : base(options)
        {
        }
    }
}
