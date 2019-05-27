namespace GitHubMemberSearch.Models
{
    public class GitHubUserReposViewModelItem
    {
        public string name { get; set; }
        public string full_name { get; set; }
        public string description { get; set; }
        public int stargazers_count { get; set; }
        public string html_url { get; set; }
    }
}