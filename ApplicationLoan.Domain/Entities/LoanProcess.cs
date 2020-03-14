using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ApplicationLoan.Domain.Entities
{
    public class LoanProcess : BaseEntity
    {
        public static string approved = "approved";
        public static string refused = "refused";

        //[Key]
        //public string Id { get; set; }
        public string IdLoanRequest { get; set; }
        public string IdStatus { get; set; }
        public string Result { get; set; }
        public string RefusedPolicy { get; set; }
        public decimal VlAmout { get; set; }
        public string IdTerms { get; set; }

        [ForeignKey("IdTerms")]
        public virtual Terms Terms { get; set; }
        [ForeignKey("IdStatus")]
        public virtual Status Status { get; set; }
        [ForeignKey("IdLoanRequest")]
        public virtual LoanRequest LoanRequest { get; set; }

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