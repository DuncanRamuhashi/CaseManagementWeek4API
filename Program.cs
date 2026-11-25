
using CaseManagementWeek4API.Data;
using CaseManagementWeek4API.Interfaces;
using CaseManagementWeek4API.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database
builder.Services.AddDbContext<CourtCaseDatabaseContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

// Dependency Injection
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<ICharge, ChargeService>();
builder.Services.AddScoped<ICourtCase, CourtCaseService>();
builder.Services.AddScoped<ICourt, CourtService>();
builder.Services.AddScoped<IDefendant, DefendantService>();
builder.Services.AddScoped<IJudge, JudgeService>();
builder.Services.AddScoped<IOriginalCase, OriginalCaseService>();
builder.Services.AddScoped<IProsecutor, ProsecutorService>();
var app = builder.Build();

// Configure HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Case Management API V1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
