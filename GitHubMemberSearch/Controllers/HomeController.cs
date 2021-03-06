﻿namespace GitHubMemberSearch.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using GitHubMemberSearch.Models;
    using GitHubMemberSearch.Service.Interfaces;

    public class HomeController : Controller
    {
        internal static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(HomeController));
        internal readonly ICallGitHubService CallGitHubService;
        private readonly IHomeModelBuilder _homeModelBuilder;

        public HomeController(ICallGitHubService callGitHubService, IHomeModelBuilder homeModelBuilder)
        {
            CallGitHubService = callGitHubService;
            _homeModelBuilder = homeModelBuilder;
            _homeModelBuilder.HomeControllerObj = this;
        }

        public ActionResult Index(string userNameSearch)
        {
            return View(HomeModelBuilder.GetUserViewSearchModel(userNameSearch));
        }

        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Search(string userNameSearch)
        {
            GitHubUserViewSearchModel gitHubUserViewSearchModel = HomeModelBuilder.GetUserViewSearchModel(userNameSearch);
            try
            {
                var gitHubUserViewModel = _homeModelBuilder.BuildSearchViewModel(userNameSearch).GetAwaiter().GetResult();
                if (!string.IsNullOrWhiteSpace(userNameSearch))
                {
                    gitHubUserViewSearchModel.UserViewModel = new List<GitHubUserViewModel>
                    {
                        gitHubUserViewModel
                    };
                }

                return View("Index", gitHubUserViewSearchModel);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return Redirect("~/ErrorHandler");
            }
        }
    }
}