using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ServiceConsolidado
{
    internal class Program
    {
        static void Main(string[] args)
        {
            HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
            var app = builder.Build();
            app.Run();

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
