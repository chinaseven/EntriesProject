using FinanceSystemBrunoTorres.Model;
using Microsoft.AspNetCore.Mvc;
using FinanceSystemBrunoTorres.Communication;
using Microsoft.AspNetCore.Routing.Template;

namespace FinanceSystemBrunoTorres.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FinancialController : ControllerBase
    {

        private readonly ILogger<FinancialController> _logger;

        public FinancialController(ILogger<FinancialController> logger)
        {
            _logger = logger;
        }

        [HttpGet("sendDummy")]
        public string sendDummy()
        {
            //ISendData sendData = new SendData();
            //sendData.send();

            var communication = new CommunicationBuilder().CreateConnectionFactory().CreateConnection().createChannel().createQueue().build();
            communication.Send("Agora vai!");
            return "Sent";

        }

        [HttpGet("GetTransactions")]
        public IEnumerable<Transaction> Get()
        {

            
            Transaction t = new Transaction();
            t.Type = "crédito";
            t.Value = "R$200";
            t.Date = DateTime.Now;
            t.Id = new Guid().ToString();
            Transaction t2 = new Transaction();
            t2.Type = "débito";
            t2.Value = "R$400";
            t2.Date = DateTime.Now;
            t2.Id = new Guid().ToString();
            List<Transaction> list = new List<Transaction>();
            list.Add(t);
            list.Add(t2);
            //IEnumerable<Transaction> transactions = list.r;

            return list;
            
        }
    }
}
