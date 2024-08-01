using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FinanceSystemBrunoTorres.Communication;
using FinanceSystemBrunoTorres.Processor;
using FinanceSystemBrunoTorres.Controllers;
using FinanceSystemBrunoTorres.Service;
namespace FinanceSystemBrunoTorres
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.
                AddHostedService<EntriesService>().
                AddSingleton<IEntriesProcessor, EntriesProcessor>().
                AddSingleton<IMessageService, MessageService>();
                
            //AddHostedService<EntriesService>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();


            app.MapControllers();

            app.Run();
            
        }
    }
}
