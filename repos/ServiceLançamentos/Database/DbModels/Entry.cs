using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.InMemory;
namespace ServiceLançamentos.Database.DbModels
{
    public class Entry
    {
        
        public int Id { get; set; }
        public string? Type { get; set; }
        public string? Value { get; set; }
        public DateTime? Date { get; set; }

    }
}
