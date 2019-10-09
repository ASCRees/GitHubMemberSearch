using AutoMapper;
using GitHubMemberSearch.Models;
using GitHubMemberSearch.Service.Exceptions;
using GitHubMemberSearch.Services;
using GitHubMemberSearch.Services.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GitHubMemberSearch.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICallGitHubService _callGitHubService;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(HomeController));

        public HomeController(ICallGitHubService callGitHubService)
        {
            _callGitHubService = callGitHubService;
        }

        public async Task<ActionResult> Index(string UserNameSearch)
        {
            GitHubUserViewSearchModel gitHubUserViewSearchModel = new GitHubUserViewSearchModel();
            if (string.IsNullOrWhiteSpace(UserNameSearch))
            {
                gitHubUserViewSearchModel.UserNameSearch = string.Empty;
            }
            else
            {
                gitHubUserViewSearchModel.UserNameSearch = UserNameSearch;
            }
            GitHubUserViewModel gitHubUserViewModel = new GitHubUserViewModel();
            try
            {
                log.Info($"Request for details of \"{UserNameSearch}\"");
                string defaultURL = System.Configuration.ConfigurationManager.AppSettings["RootUrl"] + System.Configuration.ConfigurationManager.AppSettings["UsersUrl"];
                if (!string.IsNullOrEmpty(UserNameSearch))
                {
                    try
                    {
                        GitHubUserServiceModel gitHubUserServiceModel = await _callGitHubService.CallUserAPI(string.Format(defaultURL, UserNameSearch));
                        if (gitHubUserServiceModel != null)
                        {
                            gitHubUserViewModel = Mapper.Map<GitHubUserViewModel>(gitHubUserServiceModel);

                            if (gitHubUserViewModel.id > 0)
                            {
                                List<GitHubUserReposServiceModelItem> gitHubUserReposServiceModelItem = await _callGitHubService.CallUserReposAPI(gitHubUserViewModel.repos_url);
                                if (gitHubUserReposServiceModelItem != null)
                                {
                                    if (gitHubUserReposServiceModelItem.Count > 0)
                                    {
                                        gitHubUserViewModel.reposItems = Mapper.Map<List<GitHubUserReposServiceModelItem>, List<GitHubUserReposViewModelItem>>(gitHubUserReposServiceModelItem);
                                    }
                                }
                            }
                        }
                        else
                        {
                            string noRecordsFound = $"No records found for user \"{UserNameSearch}\"";
                            log.Warn(noRecordsFound);
                            gitHubUserViewModel.messaage = noRecordsFound;
                        }
                    }
                    catch (HttpResponseException ex)
                    {
                        string noRecordsFound = $"No records found for user \"{UserNameSearch}\"";
                        log.Warn(noRecordsFound);
                        gitHubUserViewModel.messaage = noRecordsFound;
                    }
                    catch(Exception ex)
                    {
                        throw ex;
                    }
                }
                if (!string.IsNullOrWhiteSpace(UserNameSearch))
                {
                    gitHubUserViewSearchModel.UserViewModel = new List<GitHubUserViewModel>();
                    gitHubUserViewSearchModel.UserViewModel.Add(gitHubUserViewModel);
                }

                return View(gitHubUserViewSearchModel);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                return Redirect("~/ErrorHandler");
            }
        }
    }

}