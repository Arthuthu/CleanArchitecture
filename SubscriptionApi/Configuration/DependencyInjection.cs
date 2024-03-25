using MassTransit;
using Microsoft.EntityFrameworkCore;
using SubscriptionApplication.Abstractions.AppServices;
using SubscriptionApplication.Repositories;
using SubscriptionApplication.Services;
using SubscriptionInfra.Consumers;
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

		public static IServiceCollection AddMassTransitService(this IServiceCollection services, IConfiguration config)
		{
			services.AddMassTransit(busConfigurator =>
			{
				busConfigurator.SetKebabCaseEndpointNameFormatter();

				busConfigurator.AddConsumer<SubscriptionConsumer>();

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
