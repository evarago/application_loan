using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ApplicationLoan.Domain.Entities
{
    public class Terms : BaseEntity
    {
        //[Key]
        //public int Id { get; set; }
        public int Term { get; set; }
    }
}