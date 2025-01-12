using LanguageProjectBackend.Data;
using LanguageProjectBackend.Dtos;
using LanguageProjectBackend.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace EmailAzureFunction
{
    public class EmailScheduler
    {
        private readonly ILogger _logger;
        private readonly SendGridService _sender;
        private readonly IUserRepo _userRepository;

        public EmailScheduler(ILoggerFactory loggerFactory, SendGridService emailSender, IUserRepo userRepo)
        {
            _logger = loggerFactory.CreateLogger<EmailScheduler>();
            _sender = emailSender;
            _userRepository = userRepo;
        }

        [Function("SendEmail")]
        public async Task Run([TimerTrigger("* 15 10 * * * ")] TimerInfo myTimer)
        {
            _logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            if (myTimer.ScheduleStatus is not null)
            {
                _logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");
            }

            int currentDate = DateTime.UtcNow.Day; //Tracks the first of the month.
            DayOfWeek currentDay = DateTime.UtcNow.DayOfWeek;
            List<GlobalUnsubscriberDto>? unsubscribedUsers = await _sender.FetchGlobalUnsubscribers(); //List of unsubscribed users

            //Check if any users have unsubscribed.
            if (unsubscribedUsers != null)
            {
                foreach (var user in unsubscribedUsers)
                {
                    _userRepository.DeleteUserByEmail(user.Email);
                }
            }

            _sender.SendNewWordEmail("Daily"); //Send email to daily subscribers.

            //Send email to weekly subscribers
            if (currentDay == DayOfWeek.Monday)
            {
                _sender.SendNewWordEmail("Weekly");
            }

            //Send email to monthly subscribers
            if (currentDate == 1)
            {
                _sender.SendNewWordEmail("Monthly");
            }
        }
    }
}
