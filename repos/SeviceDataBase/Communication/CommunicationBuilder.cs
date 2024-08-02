using RabbitMQ;
using RabbitMQ.Client;
using System.Text;

namespace ServiceLançamentos.Communication
{


    public class CommunicationBuilder
    {
        private Communication communication = new Communication();
        public CommunicationBuilder() { 
            
        }
        public CommunicationBuilder CreateConnectionFactory()
        {
            communication.connectionFactory = new ConnectionFactory { HostName = "localhost" };
            return this;
        }

        public CommunicationBuilder CreateConnection()
        {
            communication.connection = communication.connectionFactory.CreateConnection();
            return this;
        }

        public CommunicationBuilder createChannel()
        {
            communication.channel = communication.connection.CreateModel();
            return this;
        }
        
        public CommunicationBuilder createQueue()
        {
            communication.queueDeclareOk = communication.channel.QueueDeclare(queue: "hello",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            return this;
        }

        public Communication build()
        {
            return communication;
        }


           

        public void call()
        {
            /*var factory = new ConnectionFactory { HostName = "localhost" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "hello",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
            */
      
       

        }
    }
}
