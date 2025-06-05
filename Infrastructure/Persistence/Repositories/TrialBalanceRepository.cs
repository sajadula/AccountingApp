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

        public async Task<IEnumerable<(string AccountName, string AccountType, decimal NetBalance)>> GetTrialBalanceAsync()
        {
            var result = new List<(string, string, decimal)>();

            using var command = _context.Database.GetDbConnection().CreateCommand();
            command.CommandText = "sp_GetTrialBalance";
            command.CommandType = System.Data.CommandType.StoredProcedure;

            await _context.Database.OpenConnectionAsync();
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var name = reader.GetString(0);
                var type = reader.GetString(1);
                var net = reader.GetDecimal(2);
                result.Add((name, type, net));
            }

            return result;
        }
    }
}
