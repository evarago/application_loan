using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ApplicationLoan.Domain.Entities
{
    public class BaseEntity
    {
        [Key]
        public string Id { get; set; }
        public DateTime Modificado { get; set; }
        public string StatusRow { get; set; }
        public int IdUserInsert { get; set; }
        public Nullable<int> IdUserUpdate { get; set; }
    }
}