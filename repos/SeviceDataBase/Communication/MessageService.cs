using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ServiceLançamentos.Communication;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace ServiceDataBase.Communication
{
    public class MessageService:IMessageService
    {
        public IModel channel;
        private IBasicProperties properties;
        public MessageService() {
            channel = setup();
            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;
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

        public EventingBasicConsumer Receive(IModel model, EventHandler<BasicDeliverEventArgs> e )
        {
            EventingBasicConsumer consumer = new EventingBasicConsumer(model);
            model.BasicConsume(queue: "entries_queue", false, consumer);
            consumer.Received += e;

            return consumer;
        }

        public void Send(string msg)
        {
            var body = Encoding.UTF8.GetBytes(msg);
            channel.BasicPublish(exchange: string.Empty,
                     
                     routingKey: "callback_queue",
                     false,
                     basicProperties: properties,
                     body: body);
        }
    }
}
