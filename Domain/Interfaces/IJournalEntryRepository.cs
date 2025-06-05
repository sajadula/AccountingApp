using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IJournalEntryRepository
    {
        Task<int> CreateAsync(JournalEntry entry);            
        Task<int> CreateLineAsync(JournalEntryLine line);
        Task<IEnumerable<JournalEntry>> GetAllAsync();
        Task<IEnumerable<JournalEntryLine>> GetLinesByEntryIdAsync(int journalEntryId);
    }
}
