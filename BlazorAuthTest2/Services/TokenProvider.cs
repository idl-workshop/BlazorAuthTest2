using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorAuthTest2.Services
{
    public class TokenProvider
    {
        public string XsrfToken { get; set; }
    }

    public class InitialApplicationState
    {
        public string XsrfToken { get; set; }
    }

}
