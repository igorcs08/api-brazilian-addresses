using System.Text.Json.Serialization;
using BrazilianAddresses.Api.Filters;
using BrazilianAddresses.Application;
using BrazilianAddresses.Infrastructure;
using BrazilianAddresses.Domain.Extension;
using BrazilianAddresses.Infrastructure.Migrations;
using BrazilianAddresses.Application.Services.Automapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRepository(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilters)));

builder.Services.AddScoped(provider => new AutoMapper.MapperConfiguration(cfg =>
{
    cfg.AddProfile(new AutomapperConfig());
}).CreateMapper());

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

UpdateDatabase();

app.Run();

void UpdateDatabase()
{
    var connection = builder.Configuration.GetConnection();
    var databaseName = builder.Configuration.GetDatabaseName();

    Database.CreateDatabase(connection, databaseName);

    app.MigrateDatabase();
}