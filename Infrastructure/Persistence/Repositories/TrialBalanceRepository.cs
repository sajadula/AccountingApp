using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class TrialBalanceRepository : ITrialBalanceRepository
    {
        private readonly ApplicationDbContext _context;

        public TrialBalanceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<(string AccountName, string AccountType,
                                       decimal TotalDebit, decimal TotalCredit,
                                       string BalanceType, decimal BalanceAmount)>>
            GetTrialBalanceAsync()
        {
            var result = new List<(string, string, decimal, decimal, string, decimal)>();

            // Create raw ADO command because we need to read multiple columns
            using var command = _context.Database.GetDbConnection().CreateCommand();
            command.CommandText = "sp_GetTrialBalance";
            command.CommandType = System.Data.CommandType.StoredProcedure;

            await _context.Database.OpenConnectionAsync();

            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var accountName = reader.GetString(0);        // AccountName
                var accountType = reader.GetString(1);        // AccountType
                var totalDebit = reader.GetDecimal(2);       // TotalDebit
                var totalCredit = reader.GetDecimal(3);       // TotalCredit
                var balanceType = reader.GetString(4);        // BalanceType
                var balanceAmount = reader.GetDecimal(5);       // BalanceAmount

                result.Add((accountName,
                            accountType,
                            totalDebit,
                            totalCredit,
                            balanceType,
                            balanceAmount));
            }

            await _context.Database.CloseConnectionAsync();
            return result;
        }
    }
}
