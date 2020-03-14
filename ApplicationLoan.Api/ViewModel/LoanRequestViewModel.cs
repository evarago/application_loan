using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationLoan.Api.ViewModel
{
    /// <summary>
    /// POST - Loan Request Model
    /// </summary>
    public class LoanRequestViewModel
    {
        /// <summary>
        /// Customer name
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// Customer CPF
        /// </summary>
        public string cpf { get; set; }
        /// <summary>
        /// Customer BirthDate
        /// </summary>
        public DateTime birthDate { get; set; }
        /// <summary>
        /// Request Amount
        /// </summary>
        public decimal amount { get; set; }
        /// <summary>
        /// Terms (6, 9, 12)
        /// </summary>
        public int terms { get; set; }
        /// <summary>
        /// Customer income
        /// </summary>
        public decimal income { get; set; }
    }
}