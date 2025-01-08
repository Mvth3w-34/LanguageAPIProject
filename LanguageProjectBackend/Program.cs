using LanguageProjectBackend.Data;
using LanguageProjectBackend.Services;
using Microsoft.EntityFrameworkCore;






var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<LanguageProjectContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.")));
builder.Services.AddScoped<IUserRepo, UserRepository>();
builder.Services.AddScoped<IWordRepo, WordRepository>();
builder.Services.AddScoped<IUserWordRepo, UserWordRepository>();
builder.Services.AddScoped<EmailSender>();
builder.Services.AddScoped<Translator>();


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

