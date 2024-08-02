using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
namespace FinanceSystemBrunoTorres.Communication
{
    public interface IMessageService
    {
        public IModel setup();
        public IModel setupCallBack();
        public EventingBasicConsumer Receive(IModel model, EventHandler<BasicDeliverEventArgs> e);

        public void Send(string msg);
    }
}
