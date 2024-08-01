using AtendimentoInfra.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AtendimentoApi.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationDependencyInjection(this IServiceCollection services)
        {
            //AppService
            // services.AddScoped<IUserAppService, UserAppService>();

            //Repository
            //services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }

        public static IServiceCollection AddApplicationDbContext(this IServiceCollection services,
            IConfiguration config)
        {
            services.AddDbContext<AtendimentoContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("AtendimentoConnection"),
                    assembly => assembly.MigrationsAssembly(typeof(AtendimentoContext)
                    .Assembly.FullName));
            });

            return services;
        }

        public static IServiceCollection AddCorsPolicy(this IServiceCollection services)
        {
            services.AddCors(policy =>
            {
                policy.AddPolicy("OpenCorsPolicy", opt =>
                {
                    opt
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });

            return services;
        }
    }
}
