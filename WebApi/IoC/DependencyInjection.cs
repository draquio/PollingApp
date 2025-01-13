using Application.Mappers;
using Application.Queries.Users.GetAll;
using Application.Services.Validation;
using Domain.Factories;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace WebApi.IoC
{
    public static class DependencyInjection
    {
        public static void InjectDependencies(this IServiceCollection services, IConfiguration configutarion)
        {
            // DB
            services.AddDbContext<PollingDbContext>(options =>
            {
                options.UseSqlServer(configutarion.GetConnectionString("Connection"));
            });

            // AutoMapper
            services.AddAutoMapper(typeof(AutoMapperProfile));

            // Repositories
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPollRepository, PollRepository>();
            services.AddTransient<IVoteTrackingRepository, VoteTrackingRepository>();

            // Services
            services.AddScoped<IValidationService, ValidationService>();

            // Factory
            services.AddScoped<PollFactory>();

            // MediatR
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
                typeof(GetAllUsersQueryHandler).Assembly
            ));

            // Notificacion
            services.AddTransient<INotificationService, SignalRNotificationService>();

            // Cors
            /*
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()  // Permitir cualquier origen sin credenciales
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });
            */
            // SignalR
            services.AddSignalR();

        }
    }
}
