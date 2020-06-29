using Newtonsoft.Json;

namespace GitHubMemberSearch.Services.Models
{
    public class GitHubUserServiceModel
    {
        public int Id { get; set; }

        public string login { get; set; }

        public string avatarUrl { get; set; }
        public string url { get; set; }

        public string starred_url { get; set; }

        public string name { get; set; }

        public string location { get; set; }

        public string repos_url { get; set; }

        public string message { get; set; }
    }
}