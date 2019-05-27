using GitHubMemberSearch.Service.Helper;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace GitHubMemberSearch.Service.Helper
{
    public static class HttpHandler
    {
        public static HttpClient ApiClient { get; set; }

        public static void InitializeClient()
        {
            ApiClient = HttpClientFactory.Create(new HttpCustomHandler());
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            ApiClient.DefaultRequestHeaders.Add("User-Agent", "GitHub-User-Agent");
        }

        public static async Task<T> HTTPCallClient<T>(string userURL)
        {
            string returnval = string.Empty;

            using (HttpResponseMessage response = await HttpHandler.ApiClient.GetAsync(userURL))
            {
                if (response.IsSuccessStatusCode)
                {
                    returnval = response.Content.ReadAsStringAsync().Result;

                    T result = await response.Content.ReadAsAsync<T>();

                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}