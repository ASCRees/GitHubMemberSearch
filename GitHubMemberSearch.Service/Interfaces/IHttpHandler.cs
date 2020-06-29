using System.Net.Http;
using System.Threading.Tasks;

namespace GitHubMemberSearch.Service.Interfaces
{
    public interface IHttpHandler
    {
        HttpClient ApiClient { get; set; }

        Task<T> HttpCallClient<T>(string userUrl);
        void InitializeClient();
    }
}