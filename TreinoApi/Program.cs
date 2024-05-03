using TreinoApi.Configuration;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services
.AddApplicationDependencyInjection()
.AddApplicationDbContext(config)
.AddCorsPolicy();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers();

var app = builder.Build();


app.UseHttpsRedirection();
app.UseCors("OpenCorsPolicy");
app.UseAuthorization();

app.MapControllers();

app.Run();
