using ApplicationLoan.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLoan.Domain.Interfaces.Repositories
{
    public interface ILoanProcessRepository<T> where T : BaseEntity
    {
        Task<IList<LoanProcess>> SelectByFilterAsync(Expression<Func<LoanProcess, bool>> expression);
    }
}