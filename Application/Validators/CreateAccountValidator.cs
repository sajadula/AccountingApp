using Application.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class CreateAccountValidator : AbstractValidator<CreateAccountDto>
    {
        public CreateAccountValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Account name is required");
            RuleFor(x => x.Type).NotEmpty().WithMessage("Account type is required");
        }
    }
}
