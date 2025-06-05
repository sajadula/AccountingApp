using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class CreateJournalEntryDto
    {
        public DateTime Date { get; set; }
        public string Description { get; set; } = string.Empty;
        public IList<JournalEntryLineDto> Lines { get; set; } = new List<JournalEntryLineDto>();
    }
}
