using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ApplicationLoan.Domain.Entities
{
    public class InterestRate : BaseEntity
    {
        public int StartScore { get; set; }
        public int EndScore { get; set; }
        public string IdTerm { get; set; }
        public decimal VlInterest { get; set; }

        [ForeignKey("IdTerm")]
        public virtual Terms Terms { get; set; }
    }
}