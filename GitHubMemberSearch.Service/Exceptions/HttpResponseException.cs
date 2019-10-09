using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubMemberSearch.Service.Exceptions
{
    public class HttpResponseException : Exception
    {
            public HttpResponseException(string message)
               : base(message)
            {
            }

    }
}
