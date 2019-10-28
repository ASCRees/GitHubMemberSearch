using GitHubMemberSearch.Service.Exceptions;
using GitHubMemberSearch.Services;
using GitHubMemberSearch.Services.Models;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using GitHubMemberSearch.Service.Helper;

namespace GitHubMemberSearch.UnitTests.Services
{
    [ExcludeFromCodeCoverage]

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
            string userUrl = string.Format(UsersUrl, userName);

            //Act
            Task<GitHubUserServiceModel> apiResponse = callGitHubService.CallUserApi(userUrl);
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
                string userUrl = string.Format(UsersUrl, userName);

                //Act
                Task<GitHubUserServiceModel> apiResponse = callGitHubService.CallUserApi(userUrl);
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
            string userUrl = string.Format(UsersUrl, userName);

            Task<GitHubUserServiceModel> apiResponse = callGitHubService.CallUserApi(userUrl);
            apiResponse.Wait();

            //Act
            Task<List<GitHubUserReposServiceModelItem>> apiReposResponse = callGitHubService.CallUserReposApi(apiResponse.Result.repos_url);
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

            string reposUrl = "https://github.com/ASCRees2/GitHubMemberSearch";
            Mock<ICallGitHubService> chk = new Mock<ICallGitHubService>();
            chk.Setup(x => x.CallUserApi(It.IsAny<string>()))
                .ReturnsAsync(new GitHubUserServiceModel { repos_url = reposUrl });

            var outResult = chk.Object.CallUserApi(reposUrl).GetAwaiter().GetResult();

            if (outResult != null)
            {
                //Act
                //Assert
                Assert.Throws<HttpResponseException>(() => callGitHubService.CallUserReposApi(outResult.repos_url).GetAwaiter().GetResult());
            }
            else
            {
                Assert.IsTrue(outResult is null, "Response was empty");
            }
        }

        [Test(Description = "Check that the call to the git hub api returns null for a valid repos")]
        [Category("GitHubAPI")]
        [TestCase("speacock1970")]
        public void GitHub_API_CheckAPI_CallUserReposAPIURLWithNoRepositories(string userName)
        {
            //Arrange
            HttpHandler.ApiClient = null;
            ICallGitHubService callGitHubService = new CallGitHubService();
            string userUrl = string.Format(UsersUrl, userName)+"/repos";

            //Act
            //Assert
            Assert.IsNull(callGitHubService.CallUserReposApi(userUrl).GetAwaiter().GetResult());
 
        }
    }
}