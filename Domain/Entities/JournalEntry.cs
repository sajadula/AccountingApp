using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class JournalEntry
    {
        public int JournalEntryId { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; } = string.Empty;
        public ICollection<JournalEntryLine> Lines { get; set; } = new List<JournalEntryLine>();
    }
}
