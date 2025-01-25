using Microsoft.EntityFrameworkCore;
using FestivalApp_DAL.Contexts;

var builder = WebApplication.CreateBuilder(args);

// Configure DbContext with correct migrations assembly
builder.Services.AddDbContext<FestivalDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly(typeof(FestivalDbContext).Assembly.FullName))); // 👈 Fix migration reference

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Enable Swagger UI
app.UseSwagger();
app.UseSwaggerUI();

// Ensure correct routing
app.UseRouting();

app.UseAuthorization();
app.MapControllers();
app.MapGet("/", () => "Welcome to FestivalApp API!");

app.Run();