using ApplicationLoan.Domain.Entities;
using ApplicationLoan.Domain.Interfaces.Services;
using ApplicationLoan.Infra.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLoan.Service.Services
{
    public class LoanProcessService<T> : BaseService<LoanProcess> where T : BaseEntity
    {
        private LoanProcessRepository<LoanProcess> _repository = new LoanProcessRepository<LoanProcess>();

        public async Task<IList<LoanProcess>> SearchByFilterAsync(Expression<Func<LoanProcess, bool>> expression)
        {
            return await _repository.SelectByFilterAsync(expression);
        }
    }
}