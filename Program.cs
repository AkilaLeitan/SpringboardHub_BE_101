global using SpringboardHub_BE_101.Model;
global using SpringboardHub_BE_101.Config;
global using Microsoft.EntityFrameworkCore;
global using SpringboardHub_BE_101.Data;
global using Microsoft.AspNetCore.Mvc;
global using AutoMapper;
global using System.ComponentModel.DataAnnotations;
global using Microsoft.AspNetCore.Authorization;
global using System.Security.Claims;
global using SpringboardHub_BE_101.Auth;
global using SpringboardHub_BE_101.DTO;
global using SpringboardHub_BE_101.DTO.Response;
global using SpringboardHub_BE_101.DTO.Request;
global using SpringboardHub_BE_101.Service.BatchService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using SpringboardHub_BE_101.Service.CourseService;
using SpringboardHub_BE_101.Service.StudentService;
using SpringboardHub_BE_101.Service.EnrollmentService;
using SpringboardHub_BE_101.Service.SyllabusService;
using SpringboardHub_BE_101.Service.SubjectService;
using SpringboardHub_BE_101.Service.LectureService;
using SpringboardHub_BE_101.Service.TitleService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Add Database connection
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = """Standard Authorization header using the Bearer scheme. Example "bearer {token}" """,
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.OperationFilter<SecurityRequirementsOperationFilter>();
});

//automapper
builder.Services.AddAutoMapper(typeof(Program).Assembly);
//authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:IronMan").Value!)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IBatchService, BatchService>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IEnrollmentService, EnrollmentService>();
builder.Services.AddScoped<ISyllabusService, SyllabusService>();
builder.Services.AddScoped<ISubjectService, SubjectService>();
builder.Services.AddScoped<ILectureService, LectureService>();
builder.Services.AddScoped<ITitleService, TitleService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
