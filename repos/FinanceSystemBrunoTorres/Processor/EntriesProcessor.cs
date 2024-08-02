using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceSystemBrunoTorres.Communication;
using FinanceSystemBrunoTorres.Controllers;
using System.Collections.Concurrent;

namespace FinanceSystemBrunoTorres.Processor
{
    public class EntriesProcessor(IMessageService messageService, ConcurrentQueue<string> response) : IEntriesProcessor
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
            Task.Run(() => {
                var factory = new ConnectionFactory { HostName = "localhost" };
                using var connection = factory.CreateConnection();
                using var channel = connection.CreateModel();

                channel.QueueDeclare(queue: "hello2",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += OnReceive;



                channel.BasicConsume(queue: "hello2",
                                     autoAck: true,
                                     consumer: consumer);
            }
            
               
            );

            Console.WriteLine(" [*] Waiting for messages.");
            //Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();


            /* var m = messageService.setup();
                 messageService.Receive(m, OnReceive);

                 Console.ReadKey();
            */

        }
        int i = -0;
        public async Task<string> TryGetResponse()
        {
            string s;
            string result;
            TimeSpan t = new TimeSpan();
            for (int i = 0; i < 3; i++)
            {
                if (!response.IsEmpty || t.TotalMicroseconds>100)
                {
                    t.Add(TimeSpan.FromMicroseconds(100));
                    response.TryDequeue(out s);
                    return s;
                }
                
            }
 
            return "fail";
            
            
            
        }


        private void OnReceive(object? sender, BasicDeliverEventArgs ea)
            {
                byte[] body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($" [x] Received {message}");
                response.Enqueue(message);
            }
        
    }
}
