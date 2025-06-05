using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Accounts
{
    public class CreateAccountHandler : IRequestHandler<CreateAccountCommand, int>
    {
        private readonly IAccountRepository _repository;

        public CreateAccountHandler(IAccountRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            var account = new Account
            {
                Name = request.Dto.Name,
                Type = request.Dto.Type
            };
            return await _repository.CreateAsync(account);
        }
    }


}
