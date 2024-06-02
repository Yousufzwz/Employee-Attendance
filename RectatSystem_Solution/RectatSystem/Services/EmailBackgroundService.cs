using RectatSystem.Data;
using System.Text;

namespace RectatSystem.Services;

public class EmailBackgroundService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly EmailService _emailService;
    private readonly GoogleSheetsService _googleSheetsService;

    public EmailBackgroundService(IServiceProvider serviceProvider, EmailService emailService, GoogleSheetsService googleSheetsService)
    {
        _serviceProvider = serviceProvider;
        _emailService = emailService;
        _googleSheetsService = googleSheetsService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var sheetData = await _googleSheetsService.GetSheetDataAsync("Master");
                var csv = new StringBuilder();
                foreach (var row in sheetData)
                {
                    csv.AppendLine(string.Join(",", row));
                }

                await _emailService.SendEmailAsync(
                    "adminemail@gmail.com",
                    "Daily Report",
                    $"Please find the attached report.\n\n{csv.ToString()}"
                );
            }
            catch (Exception ex)
            {
                // Log the exception (using your preferred logging framework)
                Console.WriteLine($"Background service error: {ex.Message}");
            }

            await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
        }
    }
}
