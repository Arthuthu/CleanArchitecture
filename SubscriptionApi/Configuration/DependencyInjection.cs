using Microsoft.EntityFrameworkCore;
using SubscriptionInfra.Context;

namespace SubscriptionApi.Configuration
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddApplicationDependencyInjection(this IServiceCollection services)
		{
			//Application

			//Infra

			return services;
		}

		public static IServiceCollection AddApplicationDbContext(this IServiceCollection services,
			IConfiguration config)
		{
			services.AddDbContext<SubscriptionContext>(options =>
			{
				options.UseSqlServer(config.GetConnectionString("SubscriptionConnection"),
					assembly => assembly.MigrationsAssembly(typeof(SubscriptionContext)
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
