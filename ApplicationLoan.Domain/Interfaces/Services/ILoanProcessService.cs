using ApplicationLoan.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLoan.Domain.Interfaces.Services
{
    public interface ILoanProcessService<T> where T : BaseEntity
    {
        Task<IList<LoanProcess>> SearchByFilterAsync(Expression<Func<LoanProcess, bool>> expression);
    }
}