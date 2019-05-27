using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GitHubMemberSearch.Models
{
    public class GitHubUserViewSearchModel
    {
        [Required(ErrorMessage = "User Name is required")]
        public string UserNameSearch { get; set; }

        public List<GitHubUserViewModel> UserViewModel { get; set; }
    }
}