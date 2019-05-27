using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace GitHubMemberSearch.Service.Helper
{
    public class HttpCustomHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpResponseMessage response = await base.SendAsync(request, cancellationToken);
            if (response.IsSuccessStatusCode)
                return response;

            var errorResponse = request.CreateResponse(response.StatusCode);
            return errorResponse;
        }
    }
}