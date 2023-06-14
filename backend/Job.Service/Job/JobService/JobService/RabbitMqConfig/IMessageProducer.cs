namespace JobService.RabbitMqConfig
{
    public interface IMessageProducer
    {
        void SendMessage<T>(T message);
    }
}
