using InternationalWagesManager.Models;
using Microsoft.EntityFrameworkCore;
using WebApi.Controllers;
using AutoMapper;
using System.Reflection;
using InternationalWagesManager.DAL;
using ApiContracts;
using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using InternationalWagesManager.Domain;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddCors(opt =>
{
    opt.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });             
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddDbContext<MyDbContext>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<ICurrenciesRepository, CurrenciesRepository>();
builder.Services.AddScoped<IWConditionsRepository, WConditionsRepository>();
builder.Services.AddScoped<ISalaryComponentsRepository, SalaryComponentsRepository>();
builder.Services.AddScoped<IPaymentsRepository, PaymentsRepository>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    // Global exception handler
    app.UseExceptionHandler("/error-development");
}
else
{
   app.UseExceptionHandler("/error");
}


app.UseHttpsRedirection();
app.UseCors(cors => cors
.AllowAnyMethod()
.AllowAnyHeader()
.SetIsOriginAllowed(origin => true)
.AllowCredentials()
);

app.UseAuthorization();

app.MapControllers();


app.Run();
