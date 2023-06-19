using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;

namespace ChatService
{
    public class MessageConsumerService : BackgroundService
    {
        private readonly ConnectionFactory _connectionFactory;
        private readonly IMongoCollection<UserMessage> _messagesCollection;
        private readonly ILogger<MessageConsumerService> _logger;
        private const string QueueName = "user_service";
        private readonly object _lock = new object();
        private bool _isProcessing;

        public MessageConsumerService(ConnectionFactory connectionFactory, IMongoCollection<UserMessage> messagesCollection, ILogger<MessageConsumerService> logger)
        {
            _connectionFactory = connectionFactory;
            _messagesCollection = messagesCollection;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Message consumer service is starting.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using (var connection = _connectionFactory.CreateConnection())
                    using (var channel = connection.CreateModel())
                    {
                        channel.QueueDeclare(QueueName, true, false, true, null);

                        var consumer = new EventingBasicConsumer(channel);
                        consumer.Received += async (model, ea) =>
                        {
                            try
                            {
                                var receivedMessage = JsonConvert.DeserializeObject<UserModel>(Encoding.UTF8.GetString(ea.Body.Span));

                                // Process the received message and save it to the database
                                var username = receivedMessage.Username;

                                // Acquire the lock before processing
                                lock (_lock)
                                {
                                    if (_isProcessing)
                                    {
                                        // If another message is being processed, reject the current message and requeue it
                                        channel.BasicReject(ea.DeliveryTag, true);
                                        return;
                                    }

                                    _isProcessing = true;
                                }

                                var newUser = new User
                                {
                                    Username = username
                                };
                                await newUser.SaveAsync();

                                // Save the message to the MongoDB collection
                                var userMessage = new UserMessage
                                {
                                    User = username // Assuming you want to save the received username as the message
                                };
                                await _messagesCollection.InsertOneAsync(userMessage);

                                // Release the lock after processing
                                lock (_lock)
                                {
                                    _isProcessing = false;
                                }

                                channel.BasicAck(ea.DeliveryTag, false);
                            }
                            catch (Exception ex)
                            {
                                _logger.LogError(ex, "Error processing message");
                            }
                        };

                        channel.BasicConsume(QueueName, false, consumer);

                        while (!stoppingToken.IsCancellationRequested)
                        {
                            await Task.Delay(1000, stoppingToken);
                        }

                        channel.Close();
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error consuming messages from RabbitMQ");
                }
            }

            _logger.LogInformation("Message consumer service is stopping.");
        }
    }

    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMongoCollection<UserMessage> _messagesCollection;
        private readonly object _lock = new object();

        public UserController(IMongoCollection<UserMessage> messagesCollection)
        {
            _messagesCollection = messagesCollection;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                lock (_lock)
                {
                    var userMessage = RetrieveAndDeleteMessage();

                    if (userMessage != null)
                    {
                        var username = userMessage.User;

                        // Process the retrieved username
                        // ...

                        return Ok(username);
                    }
                    else
                    {
                        return NoContent();
                    }
                }
            }
            catch (Exception error)
            {
                return StatusCode(500, new { error = error.Message });
            }
        }

        private UserMessage RetrieveAndDeleteMessage()
        {
            var userMessage = _messagesCollection.FindOneAndDelete(Builders<UserMessage>.Filter.Empty);
            return userMessage;
        }
    }
}