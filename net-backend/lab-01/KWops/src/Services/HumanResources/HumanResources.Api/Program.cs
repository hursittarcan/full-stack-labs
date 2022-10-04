using HumanResources.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
ConfigurationManager configuration = builder.Configuration;
builder.Services.AddDbContext<HumanResourcesContext>(options =>
{
    string connectionString = configuration["ConnectionString"];
    options.UseSqlServer(connectionString);
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
