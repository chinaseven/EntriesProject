using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ServiceLançamentos.Database.DbModels;

namespace ServiceLançamentos.Database
{
    public interface IEntriesDataBaseService
    {
        IEnumerable<Entry> ListAllEntries();
        Entry GetEntry(int id);
        void InsertEntry(Entry t);
        void UpdateEntry(Entry t, int id);
        void DeleteEntry(int id);


    }
}
