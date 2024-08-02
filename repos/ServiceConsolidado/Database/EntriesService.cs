using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.InMemory;
using ServiceLançamentos.Database.DbModels;

namespace ServiceLançamentos.Database
{
    public class EntriesService: IEntriesService
    {
        private readonly EntriesDbContext _context;

        public EntriesService(EntriesDbContext context)
        {
            _context = context;
        }
      
        public IEnumerable<Entry> ListAllEntries()
        {
            return _context.Entries.ToList();
        }

        public Entry GetEntry(int id)
        {
            Entry e = new Entry();
            e = _context.Entries.Where(e => e.Id == id)
                 .FirstOrDefault() != null ? e : new Entry(); 
            return e;
        }

        public void InsertEntry(Entry e)
        {
            _context.Add(e);
            _context.SaveChanges();
        }

        public void UpdateEntry(Entry e, int id)
        {         
            _context.Update(_context.Entries.Where(e => e.Id == id)
                 .FirstOrDefault() != null ? e : new Entry());
            _context.SaveChangesAsync();
        }

        public void DeleteEntry(int id)
        {
            _context.Remove(id);
            _context.SaveChanges();
        }
    }
}
