using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ServiceLançamentos.Database;
using Microsoft.EntityFrameworkCore.InMemory;
using ServiceLançamentos.Processor;
using ServiceLançamentos.Service;
using Microsoft.EntityFrameworkCore;
using ServiceLançamentos.Communication;

namespace ServiceLançamentos
{
    internal class Program
    {
        static void Main(string[] args)
        {
            HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

            builder.Services.AddHostedService<EntriesService>().
                AddScoped<IEntriesProcessor, EntriesProcessor>().
                AddScoped<IMessageService, MessageService>();
            
            using IHost host = builder.Build();

            host.Run();
            
        }
    }
}
