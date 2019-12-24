using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace GoseiVn.DemoApp.EntityFrameworkCore
{
    public static class DemoAppDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<DemoAppDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<DemoAppDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
