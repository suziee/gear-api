using Api.Gear.Interfaces;
using Api.Gear.Middleware;
using Api.Gear.Repositories;
using Dapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IDbConnectionFactory, DbConnectionFactory>();
builder.Services.AddScoped<ICamRepository, CamRepository>();
builder.Services.AddScoped<ICarabinerRepository, CarabinerRepository>();
builder.Services.AddScoped<ISlingRepository, SlingRepository>();
builder.Services.AddScoped<IStopperRepository, StopperRepository>();

SqlMapper.AddTypeHandler(new CustomGuidTypeHandler());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseExceptionHandler(builder => builder.UseCustomErrors(app.Environment));

app.UseMiddleware<CustomLogMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
