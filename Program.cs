global using SpringboardHub_BE_101.Model;
global using SpringboardHub_BE_101.Config;
global using Microsoft.EntityFrameworkCore;
global using SpringboardHub_BE_101.Data;
global using System.ComponentModel.DataAnnotations;
global using Microsoft.AspNetCore.Mvc;
global using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Add Database connection
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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
