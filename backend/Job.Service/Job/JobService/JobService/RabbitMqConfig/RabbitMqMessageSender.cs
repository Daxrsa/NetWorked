using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace JobService.RabbitMqConfig
{
    public class RabbitMqMessageSender : IMessageProducer
    {
        private readonly string _hostname;
        private readonly string _password;
        private readonly string _username;
        private IConnection _connection;

        public RabbitMqMessageSender()
        {
            _hostname = "localhost";
            _password = "guest";
            _username = "guest";
        }
        public void SendMessage<T>(T message)
        {
            if (ConnectionExists())
            {
                using IModel channel = _connection.CreateModel();
                channel.QueueDeclare("user",durable: true, exclusive: false);

                var jsonString = JsonSerializer.Serialize(message);
                var body = Encoding.UTF8.GetBytes(jsonString);
                channel.BasicPublish("", "user", body: body);
            }
            else
            {
                Console.WriteLine("Failed to connect with RabbitMQ");
            }
        }

        private void CreateConnection()
        {
            try
            {
                ConnectionFactory factory = new ConnectionFactory
                {
                    HostName = _hostname,
                    UserName = _username,
                    Password = _password
                };
                _connection = factory.CreateConnection();
                Console.WriteLine("RabbitMQ connection established.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to establish RabbitMQ connection: {ex.Message}");
            }
        }

        private bool ConnectionExists()
        {
            if (_connection != null && _connection.IsOpen)
            {
                return true;
            }
            CreateConnection();
            return _connection != null && _connection.IsOpen;
        }
    }
}
