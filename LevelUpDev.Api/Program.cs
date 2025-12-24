using System.Text.Json;
using System.Text.Json.Serialization;
using FluentValidation;
using LevelUpDev.Api.Middleware;
using LevelUpDev.Application.Validators;
using LevelUpDev.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add structured logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

// Add Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "LevelUpDev API",
        Version = "v1",
        Description = "LeetCode community platform API for ~200 developers"
    });
});

// Add Infrastructure layer (Cosmos DB, repositories)
builder.Services.AddInfrastructure(builder.Configuration);

// Add FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<RegisterUserRequestValidator>();

// Add middleware as services
builder.Services.AddTransient<GlobalExceptionHandler>();
builder.Services.AddTransient<RequestLoggingMiddleware>();

// TODO: Add Application layer services
// builder.Services.AddScoped<IUserService, UserService>();
// builder.Services.AddScoped<IUserStatsService, UserStatsService>();
// builder.Services.AddScoped<ISquadService, SquadService>();
// builder.Services.AddScoped<ILeaderboardService, LeaderboardService>();
// builder.Services.AddScoped<IChallengeService, ChallengeService>();
// builder.Services.AddScoped<INotificationService, NotificationService>();
// builder.Services.AddScoped<IQuestService, QuestService>();
// builder.Services.AddScoped<IAchievementService, AchievementService>();
// builder.Services.AddScoped<ILeetCodeSyncService, LeetCodeSyncService>();

// Add CORS for frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:3000", "https://levelupdev.azurewebsites.net")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

// Add health checks
builder.Services.AddHealthChecks();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Use custom middleware
app.UseMiddleware<RequestLoggingMiddleware>();
app.UseMiddleware<GlobalExceptionHandler>();

app.UseHttpsRedirection();

app.UseCors("AllowFrontend");

app.UseAuthorization();

// Health check endpoint
app.MapHealthChecks("/health");

app.MapControllers();

app.Run();
