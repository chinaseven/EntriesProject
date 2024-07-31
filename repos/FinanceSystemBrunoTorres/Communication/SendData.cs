using Microsoft.AspNetCore.Connections;
using System.Text;
using RabbitMQ;
using RabbitMQ.Client;

namespace FinanceSystemBrunoTorres.Communication
{
    public class SendData:ISendData
    {


        public void send()
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
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
                                         body: body);
        }
    }
}
