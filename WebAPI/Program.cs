using Application.Services;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

// Configure DbContext with In-Memory Database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("SchoolDb"));

// Register Repositories
builder.Services.AddScoped<IClassRepository, ClassRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();

// Register Services
builder.Services.AddScoped<IClassService, ClassService>();
builder.Services.AddScoped<IStudentService, StudentService>();

// Add Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "School Management API",
        Version = "v1",
        Description = "ASP.NET Core Web API - Layered Architecture Demo"
    });
});

var app = builder.Build();

// Seed the database
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
// Enable Swagger in all environments for demo/testing purposes
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "School Management API v1");
    c.RoutePrefix = "swagger"; // Swagger UI at /swagger
});

// Enable static files
app.UseStaticFiles();

// Add CORS for frontend
app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Map root to index.html
app.MapFallbackToFile("index.html");

app.Run();
