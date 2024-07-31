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

namespace ServiceLançamentos.Communication
{
    public class MessageService:IMessageService
    {
        public IModel channel;
        public IModel setup()
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            var connection = factory.CreateConnection();
            channel = connection.CreateModel();

            channel.QueueDeclare(queue: "hello",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            return channel;
            
        }


        public EventingBasicConsumer Receive(IModel model, EventHandler<BasicDeliverEventArgs> e )
        {
            EventingBasicConsumer consumer = new EventingBasicConsumer(model);
            model.BasicConsume("hello", false, consumer);
            consumer.Received += e;

            return consumer;
        }


    }
            


    
}
