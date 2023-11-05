using SportComplexResourceOptimizationApi.Persistence.PersistenceExtensions;
using SportComplexResourceOptimizationApi.Infrastructure.InfrastructureExtentions;
using SportComplexResourceOptimizationApi.Domain.Entities;
using SportComplexResourceOptimizationApi.Application.ApplicationExtentions;
using SportComplexResourceOptimizationApi.Infrastructure.Services;
using SportComplexResourceOptimizationApi.Application.IServices;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMapper();
builder.Services.AddInfrastructure();
builder.Services.AddServices();
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

//using var scope = app.Services.CreateScope();
//var serviceProvider = scope.ServiceProvider;
//var initializer = new DbInitialaizer(serviceProvider);
//await initializer.InitialaizeDb(CancellationToken.None);

app.Run();
