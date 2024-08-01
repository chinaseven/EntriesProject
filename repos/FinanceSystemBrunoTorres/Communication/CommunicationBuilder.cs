using RabbitMQ;
using RabbitMQ.Client;
using System.Text;
using System.Threading.Channels;

namespace FinanceSystemBrunoTorres.Sender
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
            communication.queueDeclareOk = communication.channel.QueueDeclare(queue: "entries_queue",
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


           

        public CommunicationBuilder createModel()
        {
              communication.channel.QueueDeclare(queue: "entries_queue",
                     durable: true,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);
            return this;
        }
    }
}
