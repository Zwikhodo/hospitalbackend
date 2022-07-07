using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace HospitalSystem.EntityFrameworkCore
{
    public static class HospitalSystemDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<HospitalSystemDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<HospitalSystemDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
