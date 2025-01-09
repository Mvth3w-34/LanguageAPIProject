using LanguageProjectBackend.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace EmailAzureFunction
{
    public class EmailScheduler
    {
        private readonly ILogger _logger;
        private readonly SendGridService _sender;

        public EmailScheduler(ILoggerFactory loggerFactory, LanguageProjectBackend.Services.SendGridService emailSender)
        {
            _logger = loggerFactory.CreateLogger<EmailScheduler>();
            _sender = emailSender;
        }

        [Function("SendEmail")]
        public void Run([TimerTrigger("* 15 10 * * * ")] TimerInfo myTimer)
        {
            _logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            if (myTimer.ScheduleStatus is not null)
            {
                _logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");
            }

            int currentDate = DateTime.UtcNow.Day;
            DayOfWeek currentDay = DateTime.UtcNow.DayOfWeek;

            _sender.SendNewWordEmail("Daily");

            //Send email to weekly subscribers
            if (currentDay == DayOfWeek.Monday)
            {
                _sender.SendNewWordEmail("Monthly");
            }

            //Send email to monthly subscribers
            if (currentDate == 1)
            {
                _sender.SendNewWordEmail("Monthly");
            }
        }
    }
}
