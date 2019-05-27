using System.Collections.Generic;

namespace GitHubMemberSearch.Models
{
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
        public string messaage { get; set; }

        public List<GitHubUserReposViewModelItem> reposItems = new List<GitHubUserReposViewModelItem>();
    }
}