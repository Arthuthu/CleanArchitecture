﻿using Microsoft.EntityFrameworkCore;
using SubscriptionApplication.Abstractions.AppServices;
using SubscriptionApplication.Repositories;
using SubscriptionApplication.Services;
using SubscriptionInfra.Context;
using SubscriptionInfra.Repositories;

namespace SubscriptionApi.Configuration
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddApplicationDependencyInjection(this IServiceCollection services)
		{
			//AppService
			services.AddScoped<ISubscriptionAppService, SubscriptionAppService>();

			//Repository
			services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();

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
