
using BLL.Services;
using BLL.Services.Auth;
using DAL.Intrfices;
using DAL.Repositreise;
using Microsoft.Extensions.DependencyInjection;

namespace BLL.Extensions
{

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IPatientRepository, PatientRepository>();
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();

            // Services
            services.AddScoped<PatientService>();
            services.AddScoped<AppointmentService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenService, TokenService>();

            // AutoMapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


            return services;
        }
    }
}


