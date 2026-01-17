using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using PoliNote.Data;
using PoliNote.Middleware;
using PoliNote.Repositories;
using PoliNote.Services.auth;
using PoliNote.Services.Auth;
using PoliNote.Services.PublicCalendar;

namespace PoliNote
{
    public class Program
    {
        public static void Main(string[] args)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            var builder = WebApplication.CreateBuilder(args);

            // Exception handler
            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
            builder.Services.AddProblemDetails();

            // CORS
            builder.Services.AddCors(options => {
                options.AddPolicy("MauiPolicy", policy => {
                    policy.WithOrigins("http://localhost:5000") // CLIENT ADDRESS
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          .AllowCredentials();
                });
            });

            // cookie
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.Cookie.Name = "PoliNoteSession";

                // error codes
                options.Events.OnRedirectToAccessDenied = context =>
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    return Task.CompletedTask;
                };

                options.Events.OnRedirectToLogin = context =>
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return Task.CompletedTask;
                };
            });

            // controllers
            builder.Services.AddControllers()
                .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
            });

            // swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // DbContext
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(
                    builder.Configuration.GetConnectionString("DefaultConnection")
                )
            );

            // repositories
            builder.Services.AddScoped<UserRepository>();
            builder.Services.AddScoped<PublicCalendarRepository>();

            // services
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<AuthService>();
            builder.Services.AddScoped<PublicEventValidator>();
            builder.Services.AddScoped<IsOwnerService>();

            // api conf
            builder.Services.Configure<RouteOptions>(options =>
            {
                options.LowercaseUrls = true;
            });

            var app = builder.Build();

            // activate Exception handler
            app.UseExceptionHandler();

            // auth
            app.UseCors("MauiPolicy");
            app.UseAuthentication();
            app.UseAuthorization();

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
