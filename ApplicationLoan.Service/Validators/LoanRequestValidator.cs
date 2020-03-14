using ApplicationLoan.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationLoan.Service.Validators
{
    public class LoanRequestValidator : AbstractValidator<LoanRequest>
    {
        public LoanRequestValidator()
        {
            RuleFor(c => c)
                .NotNull()
                .OnAnyFailure(x =>
                {
                    throw new ArgumentNullException("Can't found the object.");
                });

            RuleFor(c => c.IdCustomer)
                .NotEmpty().WithMessage("Is necessary to inform the Customer.")
                .NotNull().WithMessage("Is necessary to inform the Customer.");

            RuleFor(c => c.IdTerms)
                .NotEmpty().WithMessage("Is necessary to inform the Terms.")
                .NotNull().WithMessage("Is necessary to inform the Terms.");

            //RuleFor(c => c.VlAmout)
            //    .NotEmpty().WithMessage("Is necessary to inform the Amount.")
            //    .NotNull().WithMessage("Is necessary to inform the Amount.");

            RuleFor(c => c.VlIncome)
                .NotEmpty().WithMessage("Is necessary to inform the Income.")
                .NotNull().WithMessage("Is necessary to inform the Income.");
        }
    }
}