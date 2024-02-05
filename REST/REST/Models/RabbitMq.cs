using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace REST.Models
{
    public class RabbitMQClientService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly string _queueName;

        public RabbitMQClientService(IConfiguration configuration)
        {
            var factory = new ConnectionFactory() { Uri = new Uri("amqps://jyitxztm:t9uvBLzfma8VJfFsk8JwQVTJ9ULNT0q2@turkey.rmq.cloudamqp.com/jyitxztm") };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _queueName = "soa";
            _channel.QueueDeclare(queue: _queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
        }

        public void Consume(EventHandler<BasicDeliverEventArgs> handler)
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += handler;
            _channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);
        }

        public void Close()
        {
            _channel?.Close();
            _connection?.Close();
        }
    }

    public class RabbitMQConsumerService : BackgroundService
    {
        private readonly RabbitMQClientService _rabbitMQClientService;
        private readonly WebSocketManager _webSocketManager;

        public RabbitMQConsumerService(RabbitMQClientService rabbitMQClientService, WebSocketManager webSocketManager)
        {
            _rabbitMQClientService = rabbitMQClientService;
            _webSocketManager = webSocketManager;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            _rabbitMQClientService.Consume(async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                await _webSocketManager.SendMessageToAllAsync(message);
                Debug.Print($"[x] Sent: {message} \n");
            });

            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            _rabbitMQClientService.Close();
            base.Dispose();
        }
    }
}
