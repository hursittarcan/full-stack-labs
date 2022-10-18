using DevOps.AppLogic;
using DevOps.Infrastructure;
using HumanResources.Api.Filters;
using HumanResources.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
ConfigurationManager configuration = builder.Configuration;
builder.Services.AddDbContext<DevOpsContext>(options =>
{
    string connectionString = configuration["ConnectionString"];
    options.UseSqlServer(connectionString, sqlOptions =>
    {
        sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
    });
#if DEBUG
    options.UseLoggerFactory(LoggerFactory.Create(loggingBuilder => loggingBuilder.AddDebug()));
    options.EnableSensitiveDataLogging();
#endif
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

builder.Services.AddScoped<DevOpsDbInitializer>();
builder.Services.AddScoped<IDeveloperRepository, DeveloperRepository>();
builder.Services.AddScoped<ITeamRepository, TeamRepository>();
builder.Services.AddScoped<ITeamService, TeamService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers(options =>
{
    options.Filters.AddService<ApplicationExceptionFilterAttribute>();
});

IServiceScope startUpScope = app.Services.CreateScope();
var initializer = startUpScope.ServiceProvider.GetRequiredService<DevOpsDbInitializer>();
initializer.MigrateDatabase();
initializer.SeedData();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
