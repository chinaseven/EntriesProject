using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ServiceLançamentos.Database;
using Microsoft.EntityFrameworkCore.InMemory;
using ServiceLançamentos.Processor;
using ServiceLançamentos.Service;
using Microsoft.EntityFrameworkCore;
using ServiceLançamentos.Communication;


namespace SeviceDataBase
{
    internal class Program
    {
        static void Main(string[] args)
        {
            HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

            builder.Services.AddHostedService<EntriesService>().
                AddScoped<IEntriesProcessor, EntriesProcessor>().
                // AddScoped<IMessageService, MessageService>().
                AddScoped<IEntriesDataBaseService, EntriesDataBaseService>().
                AddDbContext<EntriesDbContext>(opt => opt.UseInMemoryDatabase("test"));

            using IHost host = builder.Build();

            host.Run();

        }

     
    }
}
