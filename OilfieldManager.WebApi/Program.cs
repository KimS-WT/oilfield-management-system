using Microsoft.EntityFrameworkCore;
using OilfieldManager.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// 1. Configure SQLite Database Connection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<OilfieldDbContext>(options =>
    options.UseSqlite(connectionString));

// 2. Enable CORS for React/Vite development
builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:3000", "http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// 3. Register Controllers and Native .NET 10 OpenAPI
builder.Services.AddControllers();
builder.Services.AddOpenApi(); 

var app = builder.Build();

// 4. Configure HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi(); // Exposes schema at /openapi/v1.json
}

app.UseHttpsRedirection();
app.UseCors("ReactPolicy");
app.UseAuthorization();
app.MapControllers();

app.Run();
