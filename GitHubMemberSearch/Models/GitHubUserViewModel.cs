using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace GitHubMemberSearch.Models
{
    [ExcludeFromCodeCoverage]
    public class GitHubUserViewModel
    {
        public int id { get; set; }

        public string login { get; set; }

        public string avatar_url { get; set; }

        public string url { get; set; }

        public string starred_url { get; set; }

        public string name { get; set; }

        public string location { get; set; }

        public string repos_url { get; set; }

        public string message { get; set; }

        public List<GitHubUserReposViewModelItem> reposItems = new List<GitHubUserReposViewModelItem>();
    }
}