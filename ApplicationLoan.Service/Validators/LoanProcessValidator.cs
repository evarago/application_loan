using ApplicationLoan.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationLoan.Service.Validators
{
    public class LoanProcessValidator : AbstractValidator<LoanProcess>
    {
        public LoanProcessValidator()
        {
            RuleFor(c => c)
                .NotNull()
                .OnAnyFailure(x =>
                {
                    throw new ArgumentNullException("Can't found the object.");
                });

            //RuleFor(c => c.IdLoanRequest)
            //    .NotEmpty().WithMessage("Is necessary to inform the Loan Request Origin.")
            //    .NotNull().WithMessage("Is necessary to inform the Loan Request Origin.");

            //RuleFor(c => c.IdTerms)
            //    .NotEmpty().WithMessage("Is necessary to inform the Terms.")
            //    .NotNull().WithMessage("Is necessary to inform the Terms.");

            //RuleFor(c => c.VlAmout)
            //    .NotEmpty().WithMessage("Is necessary to inform the Amount.")
            //    .NotNull().WithMessage("Is necessary to inform the Amount.");

            //RuleFor(c => c.IdStatus)
            //    .NotEmpty().WithMessage("Is necessary to inform the Status.")
            //    .NotNull().WithMessage("Is necessary to inform the Status.");
        }
    }
}