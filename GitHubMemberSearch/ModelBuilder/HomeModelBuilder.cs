using AutoMapper;
using GitHubMemberSearch.Models;
using GitHubMemberSearch.Service.Exceptions;
using GitHubMemberSearch.Services.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GitHubMemberSearch.Controllers
{
    public class HomeModelBuilder : IHomeModelBuilder
    {
        public HomeController HomeControllerObj { get; set; }
        public async Task<GitHubUserViewModel> BuildSearchViewModel(string UserNameSearch)
        {
            GitHubUserViewModel gitHubUserViewModel = new GitHubUserViewModel();
            HomeController.log.Info($"Request for details of \"{UserNameSearch}\"");
            string defaultURL = System.Configuration.ConfigurationManager.AppSettings["RootUrl"] +
                                System.Configuration.ConfigurationManager.AppSettings["UsersUrl"];
            if (!string.IsNullOrEmpty(UserNameSearch))
            {
                try
                {
                    GitHubUserServiceModel gitHubUserServiceModel =
                        await HomeControllerObj._callGitHubService.CallUserAPI(string.Format(defaultURL, UserNameSearch));
                    if (gitHubUserServiceModel != null)
                    {
                        gitHubUserViewModel = Mapper.Map<GitHubUserViewModel>(gitHubUserServiceModel);

                        if (gitHubUserViewModel.id > 0)
                        {
                            List<GitHubUserReposServiceModelItem> gitHubUserReposServiceModelItem =
                                await HomeControllerObj._callGitHubService.CallUserReposAPI(gitHubUserViewModel.repos_url);
                            if (gitHubUserReposServiceModelItem != null)
                            {
                                if (gitHubUserReposServiceModelItem.Count > 0)
                                {
                                    gitHubUserViewModel.reposItems =
                                        Mapper.Map<List<GitHubUserReposServiceModelItem>, List<GitHubUserReposViewModelItem>>(
                                            gitHubUserReposServiceModelItem);
                                }
                            }
                        }
                    }
                    else
                    {
                        string noRecordsFound = $"No records found for user \"{UserNameSearch}\"";
                        HomeController.log.Warn(noRecordsFound);
                        gitHubUserViewModel.message = noRecordsFound;
                    }
                }
                catch (HttpResponseException ex)
                {
                    string noRecordsFound = $"No records found for user \"{UserNameSearch}\"";
                    HomeController.log.Warn(noRecordsFound);
                    gitHubUserViewModel.message = noRecordsFound;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            return gitHubUserViewModel;
        }

        internal static GitHubUserViewSearchModel GetUserViewSearchModel(string UserNameSearch)
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

            return gitHubUserViewSearchModel;
        }
    }
}