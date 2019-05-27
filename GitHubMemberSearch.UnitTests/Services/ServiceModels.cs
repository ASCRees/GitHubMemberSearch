using GitHubMemberSearch.Services;
using GitHubMemberSearch.Services.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GitHubMemberSearch.UnitTests.Services
{
    [TestFixture]
    public class ServiceModels : BaseServiceUnitTest
    {
        [Test(Description = "Check that the user model holds the values")]
        [Category("ServiceModel")]
        public void ServiceModels_UserModel_CheckItsPopulated()
        {
            //Arrange
            GitHubUserServiceModel gitHubUserServiceModel = new GitHubUserServiceModel();

            //Act

            gitHubUserServiceModel.id = 1;
            gitHubUserServiceModel.name = "Bob Smith";
            gitHubUserServiceModel.login = "BSmith";
            gitHubUserServiceModel.location = "Timbuktu";
            gitHubUserServiceModel.starred_url = "http://api.github.com/users/bsmith/starred";
            gitHubUserServiceModel.repos_url = "http://api.github.com/users/bsmith/repos";

            //Assert
            Assert.AreEqual(gitHubUserServiceModel.name, "Bob Smith");
        }

        [Test(Description = "Check that ")]
        [Category("ServiceModel")]
        public void ServiceModels_ReposItemModel_CheckItsPopulated()
        {
            //Arrange
            GitHubUserReposServiceModelItem gitHubUserReposServiceModelItem = new GitHubUserReposServiceModelItem();

            //Act
            gitHubUserReposServiceModelItem.name = "ReposTest";
            gitHubUserReposServiceModelItem.full_name = "Repository Test Item";
            gitHubUserReposServiceModelItem.description = "Test I could add a repository item";
            gitHubUserReposServiceModelItem.stargazers_count = 5;
            gitHubUserReposServiceModelItem.html_url = "http://www.github.com/users/bsmith/ReposTest";

            //Assert
            Assert.AreEqual(gitHubUserReposServiceModelItem.name, "ReposTest");
        }
    }
}