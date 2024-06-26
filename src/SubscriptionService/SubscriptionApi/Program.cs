using SubscriptionApi.Configuration;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services
.AddApplicationDependencyInjection()
.AddApplicationDbContext(config)
.AddCorsPolicy()
.AddMassTransitService(config);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("OpenCorsPolicy");
app.UseAuthorization();

app.MapControllers();

app.Run();
