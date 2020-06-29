namespace GitHubMemberSearch.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using GitHubMemberSearch.Service.Interfaces;
    using GitHubMemberSearch.Services.Models;

    public class CallGitHubService : ICallGitHubService
    {
        private IHttpHandler _httpHandler;

        public CallGitHubService(IHttpHandler httpHandler)
        {
            _httpHandler = httpHandler;
            SetApiClient();
        }

        public async Task<GitHubUserServiceModel> CallUserApi(string userUrl)
        {
            try
            {
                return _httpHandler.HttpCallClient<GitHubUserServiceModel>(userUrl).GetAwaiter().GetResult();
            }
            catch
            {
                throw;
            }
        }



        public async Task<List<GitHubUserReposServiceModelItem>> CallUserReposApi(string userUrl)
        {
            SetApiClient();
            try
            {
                List<GitHubUserReposServiceModelItem> reposItems = _httpHandler.HttpCallClient<List<GitHubUserReposServiceModelItem>>(userUrl).GetAwaiter().GetResult();

                if (reposItems.Count > 0)
                {
                    return reposItems.OrderByDescending(c => c.stargazers_count).Take(5).ToList();
                }
                else
                {
                    return null;
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        private void SetApiClient()
        {
            if (_httpHandler.ApiClient == null)
            {
                _httpHandler.InitializeClient();
            }
        }

    }
}