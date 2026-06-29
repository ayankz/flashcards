using Flashcards.Api.Data;
using Flashcards.Api.Endpoints;
using Flashcards.Api.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/", () =>
{
    return "Flashcards API is running";
})
.WithName("GetRoot")
.WithOpenApi();

app.MapGet("/api/health", () =>
{
    var response = new HealthResponse("ok", "API checked successfully");
    return Results.Ok(response);
})
.WithName("GetHealth")
.WithOpenApi();


app.MapWordCardEndpoints();
app.MapPageScanEndpoints();

app.Run();
