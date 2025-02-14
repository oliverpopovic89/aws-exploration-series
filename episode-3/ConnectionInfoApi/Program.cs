var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSystemd();

builder.Services.AddHealthChecks();

builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.MapHealthChecks("/healthz");

app.Run();
