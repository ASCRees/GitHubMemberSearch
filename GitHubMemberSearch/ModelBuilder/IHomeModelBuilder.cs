using System.Threading.Tasks;
using GitHubMemberSearch.Models;

namespace GitHubMemberSearch.Controllers
{
    public interface IHomeModelBuilder
    {
        HomeController HomeControllerObj { get; set; }
        Task<GitHubUserViewModel> BuildSearchViewModel(string UserNameSearch);
    }
}