using Application.DTOs;
using AutoMapper;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Accounts
{
    public class GetAccountsHandler : IRequestHandler<GetAccountsQuery, IEnumerable<AccountDto>>
    {
        private readonly IAccountRepository _repository;
        private readonly IMapper _mapper;

        public GetAccountsHandler(IAccountRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AccountDto>> Handle(GetAccountsQuery request, CancellationToken cancellationToken)
        {
            var accounts = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<AccountDto>>(accounts);
        }
    }





}

