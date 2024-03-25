﻿using Microsoft.EntityFrameworkCore;
using UserApplication.Abstractions.AppServices;
using UserApplication.Abstractions.Repositories;
using UserApplication.Services;
using UserDomain.Context;
using MassTransit;
using UserInfra.Repositories;

namespace UserApi.Configuration
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddApplicationDependencyInjection(this IServiceCollection services)
		{
			//AppService
			services.AddScoped<IUserAppService, UserAppService>();

			//Repository
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

		public static IServiceCollection AddMassTransitService(this IServiceCollection services, IConfiguration config)
		{
			services.AddMassTransit(busConfigurator =>
			{
				busConfigurator.SetKebabCaseEndpointNameFormatter();

				busConfigurator.UsingRabbitMq((context, configurator) =>
				{
					configurator.Host(new Uri(config["MessageBroker:Host"]!), h =>
					{
						h.Username(config["MessageBroker:Username"]);
						h.Password(config["MessageBroker:Password"]);
					});

					configurator.ConfigureEndpoints(context);
				});
			});

			return services;
		}
	}
}
