using GitHubMemberSearch.Service.Exceptions;
using GitHubMemberSearch.Services;
using GitHubMemberSearch.Services.Models;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GitHubMemberSearch.UnitTests.Services
{
    [TestFixture]
    public class GitHubApiCallTest : BaseServiceUnitTest
    {
        [Test(Description = "Check that the call to the git hub api returns a result as a string")]
        [Category("GitHubAPI")]
        [TestCase("robconery")]
        public void GitHub_API_CheckAPI_CheckForValidUser(string userName)
        {
            //Arrange
            ICallGitHubService callGitHubService = new CallGitHubService();
            string userURL = string.Format(UsersUrl, userName);

            //Act
            Task<GitHubUserServiceModel> apiResponse = callGitHubService.CallUserAPI(userURL);
            apiResponse.Wait();
            //Assert
            Assert.IsNotEmpty(apiResponse.Result.name, "Response was empty");
        }

        [Test(Description = "Check that the call to the git hub api returns a result as a string")]
        [Category("GitHubAPI")]
        [TestCase("NotAValidUser")]
        public void GitHub_API_CheckAPI_CheckForInValidUser(string userName)
        {
            try
            {
                //Arrange
                CallGitHubService callGitHubService = new CallGitHubService();
                string userURL = string.Format(UsersUrl, userName);

                //Act
                Task<GitHubUserServiceModel> apiResponse = callGitHubService.CallUserAPI(userURL);
                apiResponse.Wait();
            }
            catch (Exception ex)
            {
                //Assert
                Assert.AreEqual(ex.InnerException.Message, "Not Found");
            }
        }

        [Test(Description = "Check that the call to the git hub api returns a result as a string")]
        [Category("GitHubAPI")]
        [TestCase("robconery")]
        public void GitHub_API_CheckAPI_CallReposURLWithResults(string userName)
        {
            //Arrange
            CallGitHubService callGitHubService = new CallGitHubService();
            string userURL = string.Format(UsersUrl, userName);

            Task<GitHubUserServiceModel> apiResponse = callGitHubService.CallUserAPI(userURL);
            apiResponse.Wait();

            //Act
            Task<List<GitHubUserReposServiceModelItem>> apiReposResponse = callGitHubService.CallUserReposAPI(apiResponse.Result.repos_url);
            apiReposResponse.Wait();

            //Assert
            Assert.IsTrue(apiReposResponse.Result.Count > 0, "Response was empty");
        }

        [Test(Description = "Check that the call to the git hub api returns a result as a string")]
        [Category("GitHubAPI")]
        [TestCase("ASCRees2")]
        public void GitHub_API_CheckAPI_CallReposURLWithNoResults(string userName)
        {
            //Arrange
            CallGitHubService callGitHubService = new CallGitHubService();
            string userURL = string.Format(UsersUrl, userName);

            string _reposUrl = "https://github.com/ASCRees2/GitHubMemberSearch";
            Mock<ICallGitHubService> chk = new Mock<ICallGitHubService>();
            chk.Setup(x => x.CallUserAPI(It.IsAny<string>()))
                .ReturnsAsync(new GitHubUserServiceModel { repos_url = _reposUrl });

            var outResult = chk.Object.CallUserAPI(_reposUrl).GetAwaiter().GetResult();

            if (outResult != null)
            {
                //Act
                //Assert
                Assert.Throws<HttpResponseException>(() => callGitHubService.CallUserReposAPI(outResult.repos_url).GetAwaiter().GetResult());

            }
            else
            {
                Assert.IsTrue(outResult is null, "Response was empty");
            }
        }
    }
}