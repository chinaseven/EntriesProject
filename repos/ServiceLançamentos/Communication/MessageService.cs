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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ServiceLançamentos.Communication
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
      

        public EventingBasicConsumer Receive(IModel model, EventHandler<BasicDeliverEventArgs> e)
        {
            EventingBasicConsumer consumer = new EventingBasicConsumer(model);
            string test = model.BasicConsume(queue: "entries_queue", false, consumer);
            consumer.Received += e;

            return consumer;
        }

        public void Send(string msg)
        {
            var body = Encoding.UTF8.GetBytes(msg);
            channelback.BasicPublish(exchange: string.Empty,

                     routingKey: "",
                     false,
            basicProperties: properties,
                     body: body);

            
        
        }


    }
}
