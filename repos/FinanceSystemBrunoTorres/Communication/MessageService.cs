using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace FinanceSystemBrunoTorres.Communication
{
    public class MessageService:IMessageService
    {
        public IModel channel;
        public IModel? channelback;
        private IBasicProperties properties;
        public MessageService() =>
            //channel = setup();
            channelback = setupCallBack();

        public IModel setupCallBack()
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            var connection = factory.CreateConnection();
            channelback = connection.CreateModel();

            channelback.QueueDeclare(queue: "hello2",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            return channelback;

        }

        public IModel setup()
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            var connection = factory.CreateConnection();
            channel = connection.CreateModel();

            channel.QueueDeclare(queue: "entries",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            return channel;

        }
      

        public EventingBasicConsumer Receive(IModel model, EventHandler<BasicDeliverEventArgs> e)
        {
            setupCallBack();
            EventingBasicConsumer consumer = new EventingBasicConsumer(model);
            //string test = model.BasicConsume(queue: "hello", false, consumer);
            string test2 = model.BasicConsume(queue: "hello2", false, consumer);
            consumer.Received += e;

            return consumer;
        }

        public void Send(string msg)
        {
            /*var body = Encoding.UTF8.GetBytes(msg);
            channel.BasicPublish(exchange: string.Empty,

                     routingKey: "",
                     false,
            basicProperties: null,
                     body: body);
            */

            var factory = new ConnectionFactory { HostName = "localhost" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "hello",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            const string message = "Hello World!";
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: string.Empty,
                                 routingKey: "hello",
                                 basicProperties: null,
                                 body: body);
            Console.WriteLine($" [x] Sent {message}");

        }
    }
}
