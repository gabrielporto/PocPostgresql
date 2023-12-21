using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PocPostgresql.Domain.Entities;
using System.Reflection.Metadata;

namespace PocPostgresql.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly PostgresqlContext db;

        public BlogsController(PostgresqlContext context)
        {
            db = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Blog>> GetAsync()
        {
            return await db.Blogs.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<Results<Ok<Blog>, NotFound>> GetAsync(Guid id)
        {
            return await db.Blogs.AsNoTracking()
                .FirstOrDefaultAsync(model => model.BlogId == id) is Blog model
                ? TypedResults.Ok(model) : TypedResults.NotFound();
        }

        [HttpPost]
        public async Task<Created<Blog>> PostAsync([FromBody] string url)
        {
            var blog = db.Blogs.Add(new Blog() { Url = url }).Entity;
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Blog/{blog.BlogId}", blog);
        }

        // PUT api/<BlogsController>/5
        [HttpPut("{id}")]
        public async Task<Results<Ok, NotFound>> PutAsync(Guid id, [FromBody] Blog blog)
        {
            var affected = await db.Blogs
                .Where(model => model.BlogId == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.BlogId, blog.BlogId)
                    .SetProperty(m => m.Url, blog.Url));

            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        }

        // DELETE api/<BlogsController>/5
        [HttpDelete("{id}")]
        public async Task<Results<Ok, NotFound>> Delete(Guid id)
        {
            var affected = await db.Blogs
                .Where(model => model.BlogId == id)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        }
    }




}
