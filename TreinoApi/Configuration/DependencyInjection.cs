using Microsoft.EntityFrameworkCore;
using TreinoInfra.Context;

namespace TreinoApi.Configuration
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddApplicationDependencyInjection(this IServiceCollection services)
		{
			//AppService

			//Repository


			return services;
		}

		public static IServiceCollection AddApplicationDbContext(this IServiceCollection services,
			IConfiguration config)
		{
			services.AddDbContext<TreinoContext>(options =>
			{
				options.UseSqlServer(config.GetConnectionString("TreinoConnection"),
					assembly => assembly.MigrationsAssembly(typeof(TreinoContext)
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
