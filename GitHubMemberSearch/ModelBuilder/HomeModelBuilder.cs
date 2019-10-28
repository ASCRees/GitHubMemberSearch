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
        public async Task<GitHubUserViewModel> BuildSearchViewModel(string userNameSearch)
        {
            GitHubUserViewModel gitHubUserViewModel = new GitHubUserViewModel();
            HomeController.Log.Info($"Request for details of \"{userNameSearch}\"");
            string defaultUrl = System.Configuration.ConfigurationManager.AppSettings["RootUrl"] +
                                System.Configuration.ConfigurationManager.AppSettings["UsersUrl"];
            if (!string.IsNullOrEmpty(userNameSearch))
            {
                try
                {
                    GitHubUserServiceModel gitHubUserServiceModel =
                        await HomeControllerObj.CallGitHubService.CallUserApi(string.Format(defaultUrl, userNameSearch));
                    if (gitHubUserServiceModel != null)
                    {
                        gitHubUserViewModel = Mapper.Map<GitHubUserViewModel>(gitHubUserServiceModel);

                        if (gitHubUserViewModel.id > 0)
                        {
                            List<GitHubUserReposServiceModelItem> gitHubUserReposServiceModelItem =
                                await HomeControllerObj.CallGitHubService.CallUserReposApi(gitHubUserViewModel.repos_url);
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
                        string noRecordsFound = $"No records found for user \"{userNameSearch}\"";
                        HomeController.Log.Warn(noRecordsFound);
                        gitHubUserViewModel.message = noRecordsFound;
                    }
                }
                catch (HttpResponseException ex)
                {
                    string noRecordsFound = $"No records found for user \"{userNameSearch}\"";
                    HomeController.Log.Warn(noRecordsFound);
                    gitHubUserViewModel.message = noRecordsFound;
                }
                catch
                {
                    throw;
                }

            }
            return gitHubUserViewModel;
        }

        internal static GitHubUserViewSearchModel GetUserViewSearchModel(string userNameSearch)
        {
            GitHubUserViewSearchModel gitHubUserViewSearchModel = new GitHubUserViewSearchModel();
            if (string.IsNullOrWhiteSpace(userNameSearch))
            {
                gitHubUserViewSearchModel.UserNameSearch = string.Empty;
            }
            else
            {
                gitHubUserViewSearchModel.UserNameSearch = userNameSearch;
            }

            return gitHubUserViewSearchModel;
        }
    }
}