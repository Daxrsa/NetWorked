using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Newtonsoft.Json;
using MongoDB.Driver;

namespace ChatService
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ConnectionFactory _connectionFactory;
        private readonly IMongoCollection<UserMessage> _messagesCollection;
        private readonly IMongoDatabase _mongoDatabase;
        private const string QueueName = "user_service";

        public UserController(IMongoDatabase mongoDatabase)
        {
            _connectionFactory = new ConnectionFactory() { HostName = "localhost" };
            _mongoDatabase = mongoDatabase;
            _messagesCollection = _mongoDatabase.GetCollection<UserMessage>("Username");
            
        }

        [HttpPost]
        public async Task<IActionResult> Post()
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
                        var receivedMessage = JsonConvert.DeserializeObject<UserModel>(Encoding.UTF8.GetString(ea.Body.Span));

                        // Process the received message and save it to the database
                        var username = receivedMessage.Username;
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

                        channel.BasicAck(ea.DeliveryTag, false);
                    };

                    channel.BasicConsume(QueueName, false, consumer);
                }

                return Ok();
            }
            catch (Exception error)
            {
                return StatusCode(500, new { error = error.Message });
            }
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var userMessage = await _messagesCollection.FindOneAndDeleteAsync("{}");

                if (userMessage == null)
                {
                    return NotFound();
                }

                return Ok(userMessage.User);
            }
            catch (Exception error)
            {
                return StatusCode(500, new { error = error.Message });
            }
        }



    }
}
