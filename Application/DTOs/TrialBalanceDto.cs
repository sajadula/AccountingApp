using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class TrialBalanceDto
    {
        public string AccountName { get; set; } = string.Empty;
        public string AccountType { get; set; } = string.Empty;
        public decimal TotalDebit { get; set; }
        public decimal TotalCredit { get; set; }
        public string BalanceType { get; set; } = string.Empty; // "Debit", "Credit", or "None"
        public decimal BalanceAmount { get; set; }
    }
}
