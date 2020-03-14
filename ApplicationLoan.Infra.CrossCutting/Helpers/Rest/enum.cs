using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationLoan.Infra.CrossCutting.Helpers.Rest
{
    public enum RestApi
    {
        NOVERDE = 0,
    }

    public enum TypeOfAuth
    {
        NOAUTH = 0,
        BEARER = 1,
        BASIC = 2,
        KEY = 3
    }
}