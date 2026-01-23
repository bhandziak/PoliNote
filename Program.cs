using FluentValidation;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using PoliNote.Data;
using PoliNote.DTOs.PrivateCalendar;
using PoliNote.DTOs.PublicCalendar;
using PoliNote.Middleware;
using PoliNote.Models.Subjects;
using PoliNote.Repositories.Calendar;
using PoliNote.Repositories.Notes;
using PoliNote.Repositories.Subjects;
using PoliNote.Repositories.Users;
using PoliNote.Services;
using PoliNote.Services.auth;
using PoliNote.Services.Auth;
using PoliNote.Services.Calendar;
using PoliNote.Services.Notes;
using PoliNote.Services.PublicCalendar;
using System.Reflection;

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
                // enum convertion
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
            builder.Services.AddScoped<PrivateCalendarRepository>();

            builder.Services.AddScoped<SubjectRepository>();
            builder.Services.AddScoped<SubjectGroupRepository>();
            builder.Services.AddScoped<EnrollmentRepository>();
            builder.Services.AddScoped<NoteRepository>();

            // services
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<AuthService>();
            builder.Services.AddScoped<IsOwnerService>();
            builder.Services.AddScoped<PrivateCalendarService>();

            // validators
            var validatorTypes = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IDataValidator<>)));

            foreach (var type in validatorTypes)
            {
                var interfaceType = type.GetInterfaces().First(i => i.GetGenericTypeDefinition() == typeof(IDataValidator<>));
                builder.Services.AddScoped(interfaceType, type);
            }
            builder.Services.AddScoped<NoteValidator>();

            // api conf
            builder.Services.Configure<RouteOptions>(options =>
            {
                options.LowercaseUrls = true;
            });

            var app = builder.Build();

            // activate Exception handler
            app.UseExceptionHandler();

            // auth
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
