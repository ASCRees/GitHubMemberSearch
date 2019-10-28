using System;

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
