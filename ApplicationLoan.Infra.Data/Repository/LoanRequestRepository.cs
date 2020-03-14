using ApplicationLoan.Domain.Entities;
using ApplicationLoan.Domain.Interfaces.Repositories;
using ApplicationLoan.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLoan.Infra.Data.Repository
{
    public class LoanProcessRepository<T> : ILoanProcessRepository<LoanProcess> where T : BaseEntity
    {
        private MySqlContext context = new MySqlContext();

        public async Task<IList<LoanProcess>> SelectByFilterAsync(Expression<Func<LoanProcess, bool>> expression)
        {
            return await context.Set<LoanProcess>()
                .Include(t => t.Terms)
                .Include(t => t.Status)
                .Include(r => r.LoanRequest)
                .ThenInclude(d => d.Customer)
                .Where(expression)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}