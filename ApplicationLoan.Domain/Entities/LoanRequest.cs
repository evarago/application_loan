using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ApplicationLoan.Domain.Entities
{
    public class LoanRequest : BaseEntity
    {
        //[Key]
        //public string Id { get; set; }
        public string IdCustomer { get; set; }
        //public Nullable<DateTime> BirthDate { get; set; }
        public decimal VlAmout { get; set; }
        public string IdTerms { get; set; }
        public decimal VlIncome { get; set; }

        [ForeignKey("IdCustomer")]
        public virtual Customer Customer { get; set; }
        [ForeignKey("IdTerms")]
        public virtual Terms Terms { get; set; }

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