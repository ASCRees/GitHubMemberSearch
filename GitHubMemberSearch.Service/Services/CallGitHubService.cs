using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GitHubMemberSearch.Service.Helper;
using GitHubMemberSearch.Services.Models;

namespace GitHubMemberSearch.Services
{
    public class CallGitHubService : ICallGitHubService
    {
        public async Task<GitHubUserServiceModel> CallUserApi(string userUrl)
        {
            try
            {
                if (HttpHandler.ApiClient == null)
                {
                    HttpHandler.InitializeClient();
                }
                return HttpHandler.HttpCallClient<GitHubUserServiceModel>(userUrl).GetAwaiter().GetResult();
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<GitHubUserReposServiceModelItem>> CallUserReposApi(string userUrl)
        {
            if (HttpHandler.ApiClient == null)
            {
                HttpHandler.InitializeClient();
            }

            List<GitHubUserReposServiceModelItem> reposItems = HttpHandler.HttpCallClient<List<GitHubUserReposServiceModelItem>>(userUrl).GetAwaiter().GetResult();

            if (reposItems.Count > 0)
            {
                return reposItems.OrderByDescending(c => c.stargazers_count).Take(5).ToList();
            }
            else
            {
                return null;
            }
        }
    }
}