using HumanResources.AppLogic;
using Microsoft.EntityFrameworkCore;
using HumanResources.Infrastructure;
using HumanResources.Domain;
using HumanResources.Api.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
ConfigurationManager configuration = builder.Configuration;
builder.Services.AddDbContext<HumanResourcesContext>(options =>
{
    string connectionString = configuration["ConnectionString"];
    options.UseSqlServer(connectionString, sqlOptions =>
    {
        sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 15,
            maxRetryDelay: TimeSpan.FromSeconds(30),
            errorNumbersToAdd: null);
    });
#if DEBUG
    options.UseLoggerFactory(LoggerFactory.Create(loggingBuilder => loggingBuilder.AddDebug()));
    options.EnableSensitiveDataLogging();
#endif
});

builder.Services.AddScoped<IEmployeeRepository, EmployeeDbRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swaggetory, EmployeeDbRepositorys();pnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<HumanResourcesDbInitializer>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeDbRepository>();
builder.Services.AddScoped<HumanResourcesDbInitializer>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddSingleton<IEmployeeFactory, Employee.Factory>();

builder.Services.AddSingleton(provider => new ApplicationExceptionFilterAttribute(provider.GetRequiredService<ILogger<ApplicationExceptionFilterAttribute>>()));
builder.Services.AddControllers(options =>
{
    options.Filters.AddService<ApplicationExceptionFilterAttribute>();
});


var app = builder.Build();

IServiceScope startUpScope = app.Services.CreateScope();
var initializer = startUpScope.ServiceProvider.GetRequiredService<HumanResourcesDbInitializer>();
initializer.MigrateDatabase();

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
