using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceLançamentos.Communication;
using ServiceLançamentos.Database;
using ServiceLançamentos.Database.DbModels;

namespace ServiceLançamentos.Processor
{
    public class EntriesProcessor( IMessageService messageService) : IEntriesProcessor
    {
        public void ProcessEntries()
        {
            
    
            //var e = new 
            //communication.AssignReadEvent(EventHandler<BasicDeliverEventArgs>);
           /*
            Entry e = new Entry();
            e.Type = "crédito";
            e.Value = "R$22200";
            e.Date = DateTime.Now;
            e.Id = 111;
            Entry e2 = new Entry();
            e2.Type = "crédito";
            e2.Value = "R$200";
            e2.Date = DateTime.Now;
            e2.Id = 222;
            Entry e3 = new Entry();
            e3.Type = "crédito";
            e3.Value = "R$200";
            e3.Date = DateTime.Now;
            e3.Id = 333;
            Entry e4 = new Entry();
            e4.Type = "crédito";
            e4.Value = "R$200";
            e4.Date = DateTime.Now;
            e4.Id = 444;
            entriesDataBaseService.InsertEntry(e);
            entriesDataBaseService.InsertEntry(e2);
            entriesDataBaseService.InsertEntry(e3);
            entriesDataBaseService.InsertEntry(e4);
           */

    

            var m = messageService.setup();
            messageService.Receive(m, OnReceive);

            Console.ReadKey();
        }

        private void OnReceive(object? sender, BasicDeliverEventArgs ea)
        {
            byte[] body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine($" [x] Received {message}");

            messageService.Send("back to received");
        }
    }
}
