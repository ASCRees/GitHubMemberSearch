using GitHubMemberSearch.Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GitHubMemberSearch.Service.Interfaces
{
    public interface ICallGitHubService
    {
        Task<GitHubUserServiceModel> CallUserApi(string userUrl);

        Task<List<GitHubUserReposServiceModelItem>> CallUserReposApi(string userUrl);
    }
}