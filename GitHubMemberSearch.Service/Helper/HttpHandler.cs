

namespace GitHubMemberSearch.Service.Helper
{
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using GitHubMemberSearch.Service.Exceptions;
    using GitHubMemberSearch.Service.Interfaces;

    public class HttpHandler : IHttpHandler
    {
        public HttpClient ApiClient { get; set; }

        public void InitializeClient()
        {
            this.ApiClient = HttpClientFactory.Create(new HttpCustomHandler());
            this.ApiClient.DefaultRequestHeaders.Accept.Clear();
            this.ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            this.ApiClient.DefaultRequestHeaders.Add("User-Agent", "GitHub-User-Agent");
        }

        public async Task<T> HttpCallClient<T>(string userUrl)
        {
            using (HttpResponseMessage response = this.ApiClient.GetAsync(userUrl).GetAwaiter().GetResult())
            {
                if (response.IsSuccessStatusCode)
                {
                    T result = await response.Content.ReadAsAsync<T>();

                    return result;
                }
                else
                {
                    throw new HttpResponseException(response.ReasonPhrase);
                }
            }
        }
    }

}