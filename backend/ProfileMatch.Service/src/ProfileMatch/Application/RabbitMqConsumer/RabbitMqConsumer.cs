using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using Newtonsoft.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Application.Services.ResultsService;
using Application.RabbitMqConsumer;
using Domain.DTOs;
using System.Threading.Channels;

namespace API.RabbitMqConsumer
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
            _channel.QueueDeclare(queue: "profile_match_service", durable: true, exclusive: false);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (ch, ea) =>
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var resultsRepo = scope.ServiceProvider.GetRequiredService<IResultsRepo>();
                    var content = Encoding.UTF8.GetString(ea.Body.ToArray());
                    ConsumerDTO dto = JsonConvert.DeserializeObject<ConsumerDTO>(content);
                    CreateResultDto resultDto = new CreateResultDto
                    {
                        ApplicantSkills = dto.ApplicantSkills,
                        ApplicationId= dto.ApplicationId,
                        JobRequirements= dto.JobRequirements
                    };
                    resultsRepo.Add(resultDto);
                    _channel.BasicAck(ea.DeliveryTag, false);
                }
            };
            _channel.BasicConsume("profile_match_service", false, consumer);
            return Task.CompletedTask;
        }
    }
}
