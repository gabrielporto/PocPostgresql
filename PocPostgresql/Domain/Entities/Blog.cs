using PocPostgresql;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Http.HttpResults;
using PocPostgresql.Models;
namespace PocPostgresql.Domain.Entities
{
    public class Blog 
    {
        public Guid BlogId { get; set; }

        public string Url { get; set; }

        public List<Post> Posts { get; } = new();
    }
}