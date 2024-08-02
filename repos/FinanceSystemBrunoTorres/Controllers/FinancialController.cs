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

        public FinancialController(ILogger<FinancialController> logger, IMessageService messageService, IEntriesProcessor processor)
        {
            _logger = logger;
            _messageService = messageService;
            _processor = processor;
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
                _messageService.Send(e);
                
                return Ok();
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
