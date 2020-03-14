using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationLoan.Api.ViewModel
{
    /// <summary>
    /// GET - Loan Response Model
    /// </summary>
    public class LoanResponseViewModel
    {
        /// <summary>
        /// Identify request
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// Status for request process
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// Description about result process
        /// </summary>
        public string result { get; set; }
        /// <summary>
        /// Description about the policy
        /// </summary>
        public string refused_policy { get; set; }
        /// <summary>
        /// Request Amout
        /// </summary>
        public decimal amount { get; set; }
        /// <summary>
        /// Terms (6, 9, 12)
        /// </summary>
        public int terms { get; set; }
    }
}