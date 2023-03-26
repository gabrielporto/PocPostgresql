using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace PocPostgresql
{
    public class PostgresqlContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }

        public PostgresqlContext(DbContextOptions<PostgresqlContext> options) : base(options)
        {

        }

    }
}
