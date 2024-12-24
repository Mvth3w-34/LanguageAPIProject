using Microsoft.EntityFrameworkCore;
using LanguageProjectBackend.Models;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.AspNetCore.Hosting.Builder;
using Microsoft.AspNetCore.Components.RenderTree;
using LanguageProjectBackend.Data;
using System.Runtime.InteropServices;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

DotNetEnv.Env.Load();



// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<LanguageProjectContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.")));
builder.Services.AddScoped<IUserRepo, UserRepository>();
builder.Services.AddScoped<IWordRepo, WordRepository>();
builder.Services.AddScoped<IUserWordRepo, UserWordRepository>();


// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
