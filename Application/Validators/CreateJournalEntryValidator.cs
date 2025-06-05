using Application.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class CreateJournalEntryValidator : AbstractValidator<CreateJournalEntryDto>
    {
        public CreateJournalEntryValidator()
        {
            RuleFor(x => x.Date).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Lines).NotEmpty().WithMessage("At least one line is required");
            RuleForEach(x => x.Lines).SetValidator(new JournalEntryLineDtoValidator());

            RuleFor(x => x).Custom((dto, ctx) =>
            {
                var totalDebit = dto.Lines.Sum(l => l.Debit);
                var totalCredit = dto.Lines.Sum(l => l.Credit);
                if (totalDebit != totalCredit)
                    ctx.AddFailure("Debit and Credit totals must match");
            });
        }
    }

    public class JournalEntryLineDtoValidator : AbstractValidator<JournalEntryLineDto>
    {
        public JournalEntryLineDtoValidator()
        {
            RuleFor(x => x.AccountId).GreaterThan(0);
            RuleFor(x => x.Debit).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Credit).GreaterThanOrEqualTo(0);
        }
    }
}
