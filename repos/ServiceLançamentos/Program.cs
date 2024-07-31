using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ServiceLançamentos.Database;
using Microsoft.EntityFrameworkCore.InMemory;
using ServiceLançamentos.Processor;
using ServiceLançamentos.Service;
using Microsoft.EntityFrameworkCore;
using ServiceLançamentos.Communication;
using ServiceLançamentos.Communication;
namespace ServiceLançamentos
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //var serviceCollection=new ServiceCollection();
            //Config(serviceCollection);
            //var serviceProvider = serviceCollection.BuildServiceProvider();
            //var entryService = serviceProvider.GetService<IEntriesService>();
            HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

            builder.Services.AddHostedService<EntriesService>().
                AddSingleton<IEntriesProcessor, EntriesProcessor>().
                AddSingleton<IMessageService, MessageService>().
                AddSingleton<IEntriesDataBaseService, EntriesDataBaseService>().
                AddDbContext<EntriesDbContext>(opt =>opt.UseInMemoryDatabase("test"));
            
            using IHost host = builder.Build();

            host.Run();
            
        }

        public static void Config(ServiceCollection services)
        {
            services.AddScoped<IEntriesDataBaseService, EntriesDataBaseService>().AddDbContext<EntriesDbContext>();
        }
    }
}
