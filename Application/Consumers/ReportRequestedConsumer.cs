using Application.IntegrationEvents;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Application.Consumers
{
    public class ReportRequestedConsumer : IConsumer<ReportRequestedEvent>
    {
        private readonly ILogger<ReportRequestedConsumer> _logger;

        public ReportRequestedConsumer(ILogger<ReportRequestedConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<ReportRequestedEvent> context)
        {
            var message = context.Message;

            _logger.LogInformation($"[RabbitMQ] Received report request: {message.RequestId} for {message.UserEmail}");
            await Task.Delay(5000);

            _logger.LogInformation($"[RabbitMQ] Report sent to {message.UserEmail}!");
        }
    }
}
