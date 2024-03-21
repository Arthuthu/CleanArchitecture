using Microsoft.EntityFrameworkCore;
using UserApplication.Abstractions.AppServices;
using UserApplication.Abstractions.Repositories;
using UserApplication.Service;
using UserDomain.Context;
using UserInfra.Repositories;

namespace UserApi.Configuration
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddApplicationDependencyInjection(this IServiceCollection services)
		{
			//Application
			services.AddScoped<IUserAppService, UserAppService>();

			//Infra
			services.AddScoped<IUserRepository, UserRepository>();

			return services;
		}

		public static IServiceCollection AddApplicationDbContext(this IServiceCollection services,
			IConfiguration config)
		{
			services.AddDbContext<UserContext>(options =>
			{
				options.UseSqlServer(config.GetConnectionString("UserConnection"),
					assembly => assembly.MigrationsAssembly(typeof(UserContext)
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
