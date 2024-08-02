using FinanceSystemBrunoTorres.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Template;
using System.Text.Json.Serialization;
using System.Text.Json;
using FinanceSystemBrunoTorres.Communication;
using FinanceSystemBrunoTorres.Processor;
using System.Net;
using System;
using RabbitMQ.Client.Events;
using FinanceSystemBrunoTorres.Service;

namespace FinanceSystemBrunoTorres.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FinancialController : ControllerBase
    {

        private readonly ILogger<FinancialController> _logger;
        private readonly IMessageService _messageService;
        private readonly IEntriesProcessor _processor;
        public event EventHandler myEvent;
        public EntriesService _entriesService;
        public FinancialController(EntriesService entriesService, IMessageService messageService, IEntriesProcessor processor)
        {

            _messageService = messageService;
            _processor = processor;
            _entriesService = entriesService;

            Task.Run(() => { _entriesService.ExecuteAsync(new CancellationToken());
                Console.ReadKey();
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Entry>> GetAsync(int id)
        {
            //_messageService.Send(id.ToString());
            Entry entry;
            return Ok();
        }

        [HttpPost("postEntry")]
        public async Task<ActionResult<Entry>> CreateAsync([FromBody]Entry entry)
        {
            if (entry != null)
            {
                string e = JsonSerializer.Serialize(entry);
                await Task<string>.Run(() => _messageService.Send(e));
                //string callbackMsg =  await _processor.TryGetResponse();
                //Task<string> t1 = new Task<string>(_processor.TryGetResponse());
                Task t2 = new Task(()=>Task.Delay(400));
                
                //await t2;
                var res = Task.WhenAny(_processor.TryGetResponse(), t2);

                return Ok(res.Result);
            }
               
            else
                return BadRequest();
        }


        [HttpPost("updateEntry")]
        public async Task<ActionResult<Entry>> UpdateAsync([FromBody] Entry entry)
        {
            if (entry != null)
            {
                string e = JsonSerializer.Serialize(entry);
                _messageService.Send(e);
                return Ok();
            }

            else
                return BadRequest();
        }
    }
}
