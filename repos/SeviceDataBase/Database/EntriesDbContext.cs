using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.Extensions.Options;
using ServiceLançamentos.Database.DbModels;
namespace ServiceLançamentos.Database
{
    public class EntriesDbContext:DbContext
    {
        public EntriesDbContext(DbContextOptions<EntriesDbContext> options) : base(options)
        {

        }

        public DbSet<Entry> Entries { get; set; }
    }
}
