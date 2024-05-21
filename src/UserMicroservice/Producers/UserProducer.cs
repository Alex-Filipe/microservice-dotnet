using System.Text;
using RabbitMQ.Client;

namespace UserMicroservice.Producers
{
    public class UserProducer(IConfiguration configuration)
    {
        private readonly IConfiguration _configuration = configuration;

        public void SendMessageUserToQueue(string queueName, string userEmail)
        {
            var factory = new ConnectionFactory
            {
                HostName = _configuration["RabbitMQ:HostName"],
                UserName = _configuration["RabbitMQ:UserName"],
                Password = _configuration["RabbitMQ:Password"]
            };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            
            channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var body = Encoding.UTF8.GetBytes(userEmail);
            // var body = JsonConvert.SerializeObject(userEmail);

            channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);
        }
    }
}
