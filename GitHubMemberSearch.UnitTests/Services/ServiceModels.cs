using GitHubMemberSearch.Service.Exceptions;
using GitHubMemberSearch.Services;
using GitHubMemberSearch.Services.Models;
using Moq;
using NUnit.Framework;
using System.Diagnostics.CodeAnalysis;

namespace GitHubMemberSearch.UnitTests.Services
{
    [ExcludeFromCodeCoverage]

    [TestFixture]
    public class ServiceModels : BaseServiceUnitTest
    {
        [Test]
        public void HttpCustomHandler_ReturnSample_ReposUrl()
        {
            string reposUrl = "https://github.com/ASCRees2/GitHubMemberSearch";
            Mock<ICallGitHubService> chk = new Mock<ICallGitHubService>();
            chk.Setup(x => x.CallUserApi(It.IsAny<string>()))
                .ReturnsAsync(new GitHubUserServiceModel { repos_url = reposUrl });

            var outResult = chk.Object.CallUserApi(reposUrl).GetAwaiter().GetResult();

            Assert.AreEqual(outResult.repos_url, reposUrl);
        }

        [Test]
        public void HttpCustomHandler_Test_Exception_Thrown()
        {
            string reposUrl = "https://github.com/ASCRees2/GitHubMemberSearch";
            Mock<ICallGitHubService> chk = new Mock<ICallGitHubService>();
            chk.Setup(x => x.CallUserApi(It.IsAny<string>()))
                .Throws(new HttpResponseException("Not Found"));

            Assert.Throws<HttpResponseException>(()=> chk.Object.CallUserApi(reposUrl).GetAwaiter().GetResult());
        }

        [Test(Description = "Check that the user model holds the values")]
        [Category("ServiceModel")]
        public void ServiceModels_UserModel_CheckItsPopulated()
        {
            //Arrange
            GitHubUserServiceModel gitHubUserServiceModel = new GitHubUserServiceModel
            {

                //Act

                Id = 1,
                name = "Bob Smith",
                login = "BSmith",
                location = "Timbuktu",
                starred_url = "http://api.github.com/users/bsmith/starred",
                repos_url = "http://api.github.com/users/bsmith/repos"
            };

            //Assert
            Assert.AreEqual(gitHubUserServiceModel.name, "Bob Smith");
        }

        [Test(Description = "Check that ")]
        [Category("ServiceModel")]
        public void ServiceModels_ReposItemModel_CheckItsPopulated()
        {
            //Arrange
            GitHubUserReposServiceModelItem gitHubUserReposServiceModelItem = new GitHubUserReposServiceModelItem
            {

                //Act
                name = "ReposTest",
                full_name = "Repository Test Item",
                description = "Test I could add a repository item",
                stargazers_count = 5,
                html_url = "http://www.github.com/users/bsmith/ReposTest"
            };

            //Assert
            Assert.AreEqual(gitHubUserReposServiceModelItem.name, "ReposTest");
        }
    }
}