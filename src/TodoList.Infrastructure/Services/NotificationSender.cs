using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using TodoList.Application.Interfaces;
using TodoList.Core.Common;

namespace TodoList.Infrastructure.Services
{
    public class NotificationSender : INotificationSender
    {
        private readonly ILogger _logger;

        public NotificationSender()
        {
            //_logger = logger;
        }

        public Task Notify(Message message)
        {
            //_logger.LogInformation($"Message {message.Title} with body {message.Text}");
            return Task.CompletedTask;
        }
    }
}
