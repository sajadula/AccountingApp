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
    public class AccountRepository : IAccountRepository
    {
        private readonly ApplicationDbContext _context;

        public AccountRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateAsync(Account account)
        {
            var idParam = new SqlParameter("@Id", System.Data.SqlDbType.Int)
            {
                Direction = System.Data.ParameterDirection.Output
            };

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_CreateAccount @Name = {0}, @Type = {1}, @Id = @Id OUTPUT",
                account.Name,
                account.Type,
                idParam);

            return (int)idParam.Value;
        }

        public async Task<IEnumerable<Account>> GetAllAsync()
        {
            return await _context.Accounts
                .FromSqlRaw("EXEC sp_GetAccounts")
                .ToListAsync();
        }
    }
}
