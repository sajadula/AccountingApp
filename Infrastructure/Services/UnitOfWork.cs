using Application.Interfaces;
using Domain.Interfaces;
using Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly IAccountRepository _accountRepo;
        private readonly IJournalEntryRepository _journalRepo;
        private readonly ITrialBalanceRepository _tbRepo;

        public UnitOfWork(
            ApplicationDbContext context,
            IAccountRepository accountRepo,
            IJournalEntryRepository journalRepo,
            ITrialBalanceRepository tbRepo)
        {
            _context = context;
            _accountRepo = accountRepo;
            _journalRepo = journalRepo;
            _tbRepo = tbRepo;
        }

        public IAccountRepository Accounts => _accountRepo;
        public IJournalEntryRepository JournalEntries => _journalRepo;
        public ITrialBalanceRepository TrialBalance => _tbRepo;

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
