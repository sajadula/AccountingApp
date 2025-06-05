using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    /// Returns (AccountName, AccountType, NetBalance)
    public interface ITrialBalanceRepository
    {
        Task<IEnumerable<(string AccountName,
                           string AccountType,
                           decimal TotalDebit,
                           decimal TotalCredit,
                           string BalanceType,
                           decimal BalanceAmount)>> GetTrialBalanceAsync();
    }
}
