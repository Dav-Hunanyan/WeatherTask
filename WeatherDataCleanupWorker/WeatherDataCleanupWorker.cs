using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Weather.Dal.Models;

namespace Worker
{
    public class WeatherDataCleanupWorker : BackgroundService
    {
        private readonly ILogger<WeatherDataCleanupWorker> logger;
        private readonly IServiceProvider serviceProvider;
        private readonly IConfiguration configuration;
        public WeatherDataCleanupWorker(ILogger<WeatherDataCleanupWorker> logger, IServiceProvider serviceProvider, IConfiguration configuration)
        {
            this.logger = logger;
            this.serviceProvider = serviceProvider;
            this.configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            logger.LogInformation("Weather data cleanup is starting.");

            while (!stoppingToken.IsCancellationRequested)
            {
                var workHour = configuration.GetValue<int>("WeatherDataCleanup:WorkHour");
                var oldDaysCount = configuration.GetValue<int>("WeatherDataCleanup:OldDaysCount");
                var currentTime = DateTime.Now;

                var nextRunTime = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, workHour, 0, 0);
                if (currentTime > nextRunTime)
                {
                    nextRunTime = nextRunTime.AddDays(1);
                }

                var timeUntilNextRun = nextRunTime - currentTime;

                logger.LogInformation($"Next run time: {nextRunTime}");
                await Task.Delay(timeUntilNextRun, stoppingToken);

                using (var scope = serviceProvider.CreateScope())
                {
                    var dbContext = new LigadatabaseContext();

                    var thresholdDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-oldDaysCount));

                    try
                    {
                        var oldWeatherData = await dbContext.WeatherDailies
                            .Where(w => w.Day < thresholdDate)
                            .ToListAsync();

                        dbContext.WeatherDailies.RemoveRange(oldWeatherData);
                        await dbContext.SaveChangesAsync();

                        logger.LogInformation($"{oldWeatherData.Count} weather data records removed.");
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "An error occurred while removing old weather data.");
                    }
                }

                await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
            }

            logger.LogInformation("Weather data cleanup worker is stopping.");
        }
    }
}
