using GitHubMemberSearch.Service.Helper;
using GitHubMemberSearch.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitHubMemberSearch.Services
{
    public class CallGitHubService : ICallGitHubService
    {
        public CallGitHubService()
        {
        }

        public async Task<GitHubUserServiceModel> CallUserAPI(string userURL)
        {
            try
            {
                if (HttpHandler.ApiClient == null)
                {
                    HttpHandler.InitializeClient();
                }
                return HttpHandler.HTTPCallClient<GitHubUserServiceModel>(userURL).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<GitHubUserReposServiceModelItem>> CallUserReposAPI(string userURL)
        {
            if (HttpHandler.ApiClient == null)
            {
                HttpHandler.InitializeClient();
            }
            List<GitHubUserReposServiceModelItem> reposItems = HttpHandler.HTTPCallClient<List<GitHubUserReposServiceModelItem>>(userURL).GetAwaiter().GetResult();
            if (reposItems.Count > 0)
            {
                return reposItems.OrderByDescending(c => c.stargazers_count).Take(5).ToList<GitHubUserReposServiceModelItem>();
            }
            else
            {
                return null;
            }
        }
    }
}