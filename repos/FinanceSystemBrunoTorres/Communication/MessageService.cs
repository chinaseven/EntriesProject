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
        public IModel channelback;
        private IBasicProperties properties;
        public MessageService()
        {
            channel = setup();
            channelback = setupCallBack();
            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;
        }

        public IModel setupCallBack()
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            var connection = factory.CreateConnection();
            channelback = connection.CreateModel();

            channelback.QueueDeclare(queue: "callback_queue",
                                 durable: true,
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

            channel.QueueDeclare(queue: "entries_queue",
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            return channel;

        }
        public IModel setupQueue()
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "entries_queue",
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

            Console.WriteLine(" [*] Waiting for messages.");

            var consumer = new EventingBasicConsumer(channel);

            channel.BasicConsume(queue: "entries_queue",
                                 autoAck: false,
                                 consumer: consumer);

            return channel;
        }

        public EventingBasicConsumer Receive(IModel model, EventHandler<BasicDeliverEventArgs> e)
        {
            EventingBasicConsumer consumer = new EventingBasicConsumer(model);
            string test = model.BasicConsume(queue: "entries_queue", false, consumer);
            string test2 = model.BasicConsume(queue: "callback_queue", false, consumer);
            consumer.Received += e;

            return consumer;
        }

        public void Send(string msg)
        {
            var body = Encoding.UTF8.GetBytes(msg);
            channel.BasicPublish(exchange: string.Empty,

                     routingKey: "",
                     false,
            basicProperties: properties,
                     body: body);



        }
    }
}
