using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.JournalEntries
{
    public class CreateJournalEntryHandler : IRequestHandler<CreateJournalEntryCommand, int>
    {
        private readonly IJournalEntryRepository _repository;

        public CreateJournalEntryHandler(IJournalEntryRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(CreateJournalEntryCommand request, CancellationToken cancellationToken)
        {
            // Validate Debit = Credit
            var totalDebit = request.Dto.Lines.Sum(l => l.Debit);
            var totalCredit = request.Dto.Lines.Sum(l => l.Credit);
            if (totalDebit != totalCredit)
                throw new Exception("Total Debit must equal Total Credit");

            // Create main JournalEntry record
            var entry = new JournalEntry
            {
                Date = request.Dto.Date,
                Description = request.Dto.Description
            };
            var entryId = await _repository.CreateAsync(entry);

            // Insert each JournalEntryLine
            foreach (var lineDto in request.Dto.Lines)
            {
                var line = new JournalEntryLine
                {
                    JournalEntryId = entryId,
                    AccountId = lineDto.AccountId,
                    Debit = lineDto.Debit,
                    Credit = lineDto.Credit
                };
                await _repository.CreateLineAsync(line);
            }

            return entryId;
        }
    }
}
