using GitHubMemberSearch.Service.Interfaces;
using GitHubMemberSearch.Services.Models;
using Moq;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace GitHubMemberSearch.UnitTests
{
    [ExcludeFromCodeCoverage]

    public class BaseServiceUnitTest
    {
        private static string _baseUrl;
        private static string _usersUrl;
        private static string _starredUrl;
        public Mock<IHttpHandler> _mockHttpHandler;

        public static string BaseUrl
        {
            get
            {
                if (_baseUrl == null)
                    _baseUrl = ConfigurationManager.AppSettings["RootUrl"] + ConfigurationManager.AppSettings["BaseUrl"];

                return _baseUrl;
            }
        }

        public static string UsersUrl
        {
            get
            {
                if (_usersUrl == null)
                    _usersUrl = ConfigurationManager.AppSettings["UsersUrl"];

                return _usersUrl;
            }
        }

        public void BuildMockHeader(string userName)
        {
            var gitHubUserServiceModel = new GitHubUserServiceModel
            {
                Id = 1,
                login = userName,
                avatarUrl = $"http://api.github.com/ {userName}.jpg",
                name = userName,
                location = "London",
                repos_url = "http://api.github.com/repos/" + userName
            };

            SetupMockHandler<GitHubUserServiceModel>(gitHubUserServiceModel);
        }

        public void BuildMockHeaderNoResults(string userName)
        {
            var gitHubUserServiceModel = new GitHubUserServiceModel();

            SetupMockHandler(gitHubUserServiceModel);
        }

        public void BuildMockReposLines()
        {
            var gitHubUserReposServiceModelItem = new List<GitHubUserReposServiceModelItem>
            {
                new GitHubUserReposServiceModelItem{
                 name ="Repos1",
                 description="This is a Repos1",
                 stargazers_count=100,
                 html_url = "/Repos1",
                },
                new GitHubUserReposServiceModelItem{
                 name ="Repos2",
                 description="This is a Repos2",
                 stargazers_count=90,
                 html_url = "/Repos2",
                },
            };

            SetupMockListHandler(gitHubUserReposServiceModelItem);
        }

        public void BuildMockReposNoLines()
        {
            var gitHubUserReposServiceModelItem = new List<GitHubUserReposServiceModelItem>();

            SetupMockListHandler(gitHubUserReposServiceModelItem);
        }

        private void SetupMockHandler<T>(T model)
        {
            _mockHttpHandler.Setup(c => c.HttpCallClient<T>(It.IsAny<string>())).Returns(Task.FromResult(model));
        }
 
        private void SetupMockListHandler<T>(List<T> model)
        {
            _mockHttpHandler.Setup(c => c.HttpCallClient<List<T>>(It.IsAny<string>())).Returns(Task.FromResult(model));
        }
    }
}