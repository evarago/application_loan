using ApplicationLoan.Domain.Entities;
using ApplicationLoan.Domain.Interfaces;
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
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        private MySqlContext context = new MySqlContext();

        public void Insert(T obj)
        {
            context.Set<T>().Add(obj);
            context.SaveChanges();
        }

        public void Update(T obj)
        {
            context.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            context.Set<T>().Remove(Select(id));
            context.SaveChanges();
        }

        public IList<T> Select()
        {
            return context.Set<T>().AsNoTracking().ToList();
        }

        public async Task<T> SelectAsync(string id)
        {
            return await context.Set<T>().FindAsync(id);
        }

        public async Task<IList<T>> SelectAsync(Expression<Func<T, bool>> expression)
        {
            return await context.Set<T>().Where(expression).AsNoTracking().ToListAsync();
        }

        public T Select(int id)
        {
            return context.Set<T>().Find(id);
        }
    }
}