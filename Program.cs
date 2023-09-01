using Microsoft.EntityFrameworkCore;
using WebAPiCaching.Infrastructure;
using WebAPiCaching.Models;
using WebAPiCaching.Services;

namespace WebAPiCaching
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
            builder.Services.AddDbContext<EmployeeDbContext>(options => {
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DbConnection"));
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });
            builder.Services.AddScoped<IEmployeeService<Employee, int>, EmployeeService>();
            builder.Services.AddScoped<ICacheService, CacheService>();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}