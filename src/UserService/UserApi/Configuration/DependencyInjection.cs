using Microsoft.EntityFrameworkCore;
using UserApplication.Abstractions.AppServices;
using UserApplication.Abstractions.Repositories;
using UserApplication.Services;
using UserDomain.Context;
using MassTransit;
using UserInfra.Repositories;
using Microsoft.AspNetCore.Identity;
using UserInfra.Context.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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

			//Others
			services.AddScoped<IAuthenticate, AuthenticateService>();

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

			services.AddIdentity<IdentityUser, IdentityRole>()
				.AddEntityFrameworkStores<UserContext>()
				.AddDefaultTokenProviders();

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

		public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration config)
		{
			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(options =>
				{
					string? securitKey = config["Jwt:Key"];
					if (string.IsNullOrEmpty(securitKey))
					{
						throw new Exception("Error authenticating the user, the JwtKey value was not found");
					}

					options.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuer = true,
						ValidateAudience = true,
						ValidateLifetime = true,
						ValidateIssuerSigningKey = true,
						ValidIssuer = config["Jwt:Issuer"],
						ValidAudience = config["Jwt:Issuer"],
						IssuerSigningKey = new SymmetricSecurityKey(
							Encoding.UTF8.GetBytes(securitKey))
					};
				});

			return services;
		}
	}
}
