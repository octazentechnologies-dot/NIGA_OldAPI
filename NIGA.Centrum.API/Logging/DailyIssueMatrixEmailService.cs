using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace NIGA.Centrum.API.Logging
{
    /// <summary>
    /// At local midnight, emails the issue matrix for the calendar day that just ended.
    /// ErrorAlert:DailyMatrixEnabled turns this job on or off. Instant error emails are separate.
    /// </summary>
    public class DailyIssueMatrixEmailService : BackgroundService
    {
        private readonly ILogger<DailyIssueMatrixEmailService> _logger;

        public DailyIssueMatrixEmailService(ILogger<DailyIssueMatrixEmailService> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (!AppFileLog.IsDailyMatrixEnabled)
            {
                _logger.LogInformation("Daily issue matrix email is off (ErrorAlert:DailyMatrixEnabled=false).");
                return;
            }

            var preview = DailyIssueMatrix.Read(AppFileLog.LogsDirectory, DateTime.Today, "Old API");
            _logger.LogInformation(
                "Daily issue matrix email is on. Next send is local midnight. Issues logged so far today: {Count}.",
                preview.Sum(row => row.Count));
            foreach (var row in preview)
            {
                _logger.LogInformation(
                    "Daily matrix preview {Source} x{Count}: {RootCause}",
                    row.Source, row.Count, row.RootCause);
            }

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await Task.Delay(DelayUntilNextLocalMidnight(DateTime.Now), stoppingToken);
                    var day = DateTime.Now.Date.AddDays(-1);
                    AppFileLog.SendDailyMatrix(day);
                    _logger.LogInformation("Daily issue matrix email sent for {Day}.", day.ToString("dd-MMM-yyyy"));
                }
                catch (OperationCanceledException)
                {
                    if (stoppingToken.IsCancellationRequested)
                        break;
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Daily issue matrix email failed.");
                }
            }
        }

        /// <summary>Wait until the next local 00:00.</summary>
        public static TimeSpan DelayUntilNextLocalMidnight(DateTime now)
        {
            var next = now.Date.AddDays(1);
            var delay = next - now;
            return delay < TimeSpan.FromSeconds(1) ? TimeSpan.FromDays(1) : delay;
        }
    }
}
