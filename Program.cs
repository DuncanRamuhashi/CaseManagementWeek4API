using CaseManagementWeek4API.Data;
using CaseManagementWeek4API.Interfaces;
using CaseManagementWeek4API.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
// SQL Server and API-owned migration assembly
// Register database connection
builder.Services.AddDbContext<CourtCaseDatabaseContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));
//You need to register ICharge and ChargeService in dependency injection
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<ICharge, ChargeService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
// Redirect homepage to API documentation
app.MapGet("/", () => Results.Redirect("/openapi/v1.json"));
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
