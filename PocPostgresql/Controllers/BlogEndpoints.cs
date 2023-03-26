using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using PocPostgresql;
namespace PocPostgresql.Controllers;

public static class BlogEndpoints
{
    public static void MapBlogEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Blog").WithTags(nameof(Blog));

        group.MapGet("/", async (PostgresqlContext db) =>
        {
            return await db.Blogs.ToListAsync();
        })
        .WithName("GetAllBlogs")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Blog>, NotFound>> (int id, PostgresqlContext db) =>
        {
            return await db.Blogs.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is Blog model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetBlogById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, Blog blog, PostgresqlContext db) =>
        {
            var affected = await db.Blogs
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.Id, blog.Id)
                  .SetProperty(m => m.Name, blog.Name)
                );

            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateBlog")
        .WithOpenApi();

        group.MapPost("/", async (Blog blog, PostgresqlContext db) =>
        {
            db.Blogs.Add(blog);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Blog/{blog.Id}",blog);
        })
        .WithName("CreateBlog")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, PostgresqlContext db) =>
        {
            var affected = await db.Blogs
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();

            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteBlog")
        .WithOpenApi();
    }
}
