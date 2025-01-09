using LanguageProjectBackend.Data;
using LanguageProjectBackend.Services;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

// Application Insights isn't enabled by default. See https://aka.ms/AAt8mw4.
// builder.Services
//     .AddApplicationInsightsTelemetryWorkerService()
//     .ConfigureFunctionsApplicationInsights();

builder.Services.AddDbContext<LanguageProjectContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.")));
builder.Services.AddScoped<IUserRepo, UserRepository>();
builder.Services.AddScoped<IWordRepo, WordRepository>();
builder.Services.AddScoped<IUserWordRepo, UserWordRepository>();
builder.Services.AddScoped<SendGridService>();
builder.Services.AddScoped<Translator>();

builder.Build().Run();
