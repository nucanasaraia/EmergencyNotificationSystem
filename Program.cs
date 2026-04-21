using EmergencyNotifRespons.Configurations;
using EmergencyNotifRespons.Data;
using EmergencyNotifRespons.Extensions;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();
builder.Host.UseSerilog();

// Services
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(
            new System.Text.Json.Serialization.JsonStringEnumConverter(
                System.Text.Json.JsonNamingPolicy.SnakeCaseUpper)); 
    });
builder.Services.ConfigureDatabase(builder.Configuration);
builder.Services.ConfigureServices();
builder.Services.ConfigureSwagger();
builder.Services.ConfigureValidation();
builder.Services.ConfigureMapping();
builder.Services.ConfigureJwt(builder.Configuration);
builder.Services.AddAuthorization();
builder.Services.Configure<SmtpSettings>(
    builder.Configuration.GetSection("SMTP"));


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    try
    {
        var db = scope.ServiceProvider.GetRequiredService<DataContext>();
        db.Database.Migrate();
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}

// Middleware
app.UseAuthentication();
app.UseAuthorization();

app.ConfigureMiddleware();

app.Run();