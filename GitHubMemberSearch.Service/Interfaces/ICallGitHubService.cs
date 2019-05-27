using GitHubMemberSearch.Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GitHubMemberSearch.Services
{
    public interface ICallGitHubService
    {
        Task<GitHubUserServiceModel> CallUserAPI(string userURL);

        Task<List<GitHubUserReposServiceModelItem>> CallUserReposAPI(string userURL);
    }
}