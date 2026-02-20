using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using TestUAA2;
using TestUAA2.Services;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Injection du service Email
builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Génère le document OpenAPI (le fichier .json)
    app.MapOpenApi();
    // Affiche l'interface Scalar pour tester l'API
    app.MapScalarApiReference();
   

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
