using AutoMapper;
using GitHubMemberSearch.Models;
using GitHubMemberSearch.Service.Exceptions;
using GitHubMemberSearch.Services;
using GitHubMemberSearch.Services.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.WebPages;

namespace GitHubMemberSearch.Controllers
{
    public class HomeController : Controller
    {
        internal readonly ICallGitHubService _callGitHubService;
        internal static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(HomeController));
        private readonly IHomeModelBuilder _homeModelBuilder;

        public HomeController(ICallGitHubService callGitHubService, IHomeModelBuilder homeModelBuilder)
        {
            _callGitHubService = callGitHubService;
            _homeModelBuilder = homeModelBuilder;
            _homeModelBuilder.HomeControllerObj = this;
        }

        public ActionResult Index(string UserNameSearch)
        {
            return View(HomeModelBuilder.GetUserViewSearchModel(UserNameSearch));
        }

        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Search(string UserNameSearch)
        {
            GitHubUserViewSearchModel gitHubUserViewSearchModel = HomeModelBuilder.GetUserViewSearchModel(UserNameSearch);
            try
            {
                var gitHubUserViewModel = _homeModelBuilder.BuildSearchViewModel(UserNameSearch).GetAwaiter().GetResult();
                if (!string.IsNullOrWhiteSpace(UserNameSearch))
                {
                    gitHubUserViewSearchModel.UserViewModel = new List<GitHubUserViewModel>();
                    gitHubUserViewSearchModel.UserViewModel.Add(gitHubUserViewModel);
                }

                return View("Index",gitHubUserViewSearchModel);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                return Redirect("~/ErrorHandler");
            }
        }
    }

}