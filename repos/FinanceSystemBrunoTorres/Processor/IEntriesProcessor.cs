using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceSystemBrunoTorres.Processor
{
    public interface IEntriesProcessor
    {
        void ProcessEntries();

    }
}
