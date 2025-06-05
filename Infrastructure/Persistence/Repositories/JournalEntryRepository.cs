using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class JournalEntryRepository : IJournalEntryRepository
    {
        private readonly ApplicationDbContext _context;

        public JournalEntryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateAsync(JournalEntry entry)
        {
            var idParam = new SqlParameter("@Id", System.Data.SqlDbType.Int)
            {
                Direction = System.Data.ParameterDirection.Output
            };

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_CreateJournalEntry @Date = {0}, @Description = {1}, @Id = @Id OUTPUT",
                entry.Date,
                entry.Description,
                idParam);

            return (int)idParam.Value;
        }

        public async Task<int> CreateLineAsync(JournalEntryLine line)
        {
            return await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_CreateJournalEntryLine @JournalEntryId = {0}, @AccountId = {1}, @Debit = {2}, @Credit = {3}",
                line.JournalEntryId,
                line.AccountId,
                line.Debit,
                line.Credit);
        }

        public async Task<IEnumerable<JournalEntry>> GetAllAsync()
        {
            return await _context.JournalEntries
                .FromSqlRaw("EXEC sp_GetJournalEntries")
                .ToListAsync();
        }

        public async Task<IEnumerable<JournalEntryLine>> GetLinesByEntryIdAsync(int journalEntryId)
        {
            return await _context.JournalEntryLines
                .FromSqlRaw("EXEC sp_GetJournalEntryLines @JournalEntryId = {0}", journalEntryId)
                .ToListAsync();
        }
    }
}
