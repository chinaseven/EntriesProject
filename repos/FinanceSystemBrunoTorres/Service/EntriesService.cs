using Microsoft.Extensions.Hosting;
using FinanceSystemBrunoTorres.Processor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceSystemBrunoTorres.Service
{
    public sealed class EntriesService(IEntriesProcessor entriesProcessor) :BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                entriesProcessor.ProcessEntries();
                await Task.Delay(1_000, stoppingToken);
            }
        }
    }
}
