using ChatService.Hubs;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Diagnostics;
using System.Text;

namespace ChatService.RabbitMQConsumer
{
    public class RabbitMqConsumer : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private IConnection _connection;
        private IModel _channel;
        

        public RabbitMqConsumer(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
         
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "user_service", durable: true, exclusive: false);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (ch, ea) =>
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                  
                    var content = Encoding.UTF8.GetString(ea.Body.ToArray());
                    ConsumerDTO dto = JsonConvert.DeserializeObject<ConsumerDTO>(content);
                    UserConnection resultDto = new UserConnection
                    {
                        User = dto.Username,
                      
                    };
                    
                    Debug.WriteLine(resultDto.User);
              
                    _channel.BasicAck(ea.DeliveryTag, false);
                }
            };
            _channel.BasicConsume("user_service", false, consumer);
            return Task.CompletedTask;
        }
    }
}
