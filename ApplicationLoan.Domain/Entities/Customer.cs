using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationLoan.Domain.Entities
{
    public class Customer : BaseEntity
    {
        //public string Id { get; set; }
        public string CpfCnpj { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Create new Id to this entity
        /// </summary>
        /// <returns></returns>
        public static string GetNewId()
        {
            return Guid.NewGuid().ToString().Replace("-", "").ToLower(); ;
        }
    }
}