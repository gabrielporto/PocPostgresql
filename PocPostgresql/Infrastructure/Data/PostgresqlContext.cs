using Microsoft.EntityFrameworkCore;
using PocPostgresql.Domain.Entities;
using System.Reflection.Metadata;

namespace PocPostgresql
{
    public class PostgresqlContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        public PostgresqlContext(DbContextOptions<PostgresqlContext> options) : base(options)
        {

        }

    }
}
