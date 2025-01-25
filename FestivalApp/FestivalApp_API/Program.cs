using Microsoft.EntityFrameworkCore;
using FestivalApp_DAL.Contexts;

var builder = WebApplication.CreateBuilder(args);

// ✅ Connect to existing Azure database (No migrations needed)
builder.Services.AddDbContext<FestivalDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        policy => policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting();
app.UseCors("AllowAllOrigins");

app.UseAuthorization();
app.MapControllers();
app.MapGet("/", () => "Welcome to FestivalApp API!");

app.Run();