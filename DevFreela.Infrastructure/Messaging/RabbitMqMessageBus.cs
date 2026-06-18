using System.Text;
using System.Text.Json;
using DevFreela.Core.Messaging;
using RabbitMQ.Client;

namespace DevFreela.Infrastructure.Messaging
{
    public class RabbitMqMessageBus : IMessageBus
    {
        private readonly RabbitMqOptions _options;

        public RabbitMqMessageBus(RabbitMqOptions options)
        {
            _options = options;
        }

        public Task PublishAsync<T>(string queue, T message, CancellationToken cancellationToken = default)
        {
            var queueName = string.IsNullOrWhiteSpace(queue) ? _options.QueueName : queue;
            var factory = new ConnectionFactory
            {
                HostName = _options.HostName,
                UserName = _options.UserName,
                Password = _options.Password
            };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(
                queue: queueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var payload = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(payload);

            channel.BasicPublish(
                exchange: string.Empty,
                routingKey: queueName,
                basicProperties: null,
                body: body);

            return Task.CompletedTask;
        }
    }
}
