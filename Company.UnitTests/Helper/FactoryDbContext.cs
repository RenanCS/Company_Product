using Company.Infrastructure.Persistence;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Company.UnitTests.Helper
{
    public class FactoryDbContext
    {
        public static DbContextOptions<CompanyDbContext> CreateConnection()
        {

            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = ":memory:" };
            var connection = new SqliteConnection(connectionStringBuilder.ToString());
            var options = new DbContextOptionsBuilder<CompanyDbContext>().UseSqlite(connection).Options;
            return options;
        }

    }
}
