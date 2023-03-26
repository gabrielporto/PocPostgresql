
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PocPostgresql.Controllers;

namespace PocPostgresql
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //builder.Services.AddDbContext<PostgresqlContext>();
          builder.Services.AddEntityFrameworkNpgsql()
            .AddDbContext<PostgresqlContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresqlDB")));


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

                        if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
};

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

                        app.MapBlogEndpoints();

            app.Run();
        }
    }
}