
using Worker;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<WeatherDataCleanupWorker>();

var host = builder.Build();
host.Run();
