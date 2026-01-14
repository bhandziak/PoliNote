using Microsoft.EntityFrameworkCore;
using PoliNote.Data;

namespace PoliNote
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // controllers
            builder.Services.AddControllers();

            // swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // DbContext
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(
                    builder.Configuration.GetConnectionString("DefaultConnection")
                )
            );

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "PoliNote API V1");
                c.RoutePrefix = string.Empty;
            });

            app.MapControllers();

            app.Run();
        }
    }
}
