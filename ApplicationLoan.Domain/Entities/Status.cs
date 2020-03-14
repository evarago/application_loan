using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ApplicationLoan.Domain.Entities
{
    public class Status : BaseEntity
    {
        public static string Processing = "3e63f0f13b8643c783d679f1081f854e";
        public static string Completed = "87a53a3b12ac48afae005fc628923bf3";

        //[Key]
        //public int Id { get; set; }
        public string Description { get; set; }
    }
}