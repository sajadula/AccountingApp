using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IAccountRepository
    {
        Task<int> CreateAsync(Account account);       
        Task<IEnumerable<Account>> GetAllAsync();
    }
}
