using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationLoan.Infra.CrossCutting.Helpers.Auth
{
    public class RetornoToken
    {
        public string Access_token { get; set; }
        public string Token_type { get; set; }
    }
}