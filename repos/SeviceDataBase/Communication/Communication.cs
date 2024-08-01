using RabbitMQ;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace ServiceLançamentos.Communication
{

    public class Communication
    {
        public ConnectionFactory connectionFactory { get; set; }
        public IConnection connection { get; set; }
        public IModel channel { get; set; }
        public QueueDeclareOk queueDeclareOk { get; set; }

        

    }
}


/* var factory = new ConnectionFactory { HostName = "localhost" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "hello",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            const string message = "Munstafara!";
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: string.Empty,
                                         routingKey: "hello",
                                         basicProperties: null,
                                         body: body);*/