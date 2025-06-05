using Application.DTOs;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Application.Features.TrialBalance
{
    public class GetTrialBalanceHandler : IRequestHandler<GetTrialBalanceQuery, IEnumerable<TrialBalanceDto>>
    {
        private readonly ITrialBalanceRepository _repository;

        public GetTrialBalanceHandler(ITrialBalanceRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TrialBalanceDto>> Handle(GetTrialBalanceQuery request, CancellationToken cancellationToken)
        {
            var raw = await _repository.GetTrialBalanceAsync();
            return raw.Select(x => new TrialBalanceDto
            {
                AccountName = x.AccountName,
                AccountType = x.AccountType,
                NetBalance = x.NetBalance
            });
        }
    }
}