using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using GitHubMemberSearch.Service.Exceptions;
using GitHubMemberSearch.Service.Helper;
using GitHubMemberSearch.Service.Interfaces;
using GitHubMemberSearch.Services;
using GitHubMemberSearch.Services.Models;
using Moq;
using NUnit.Framework;

namespace GitHubMemberSearch.UnitTests.Services
{
    [ExcludeFromCodeCoverage]

    [TestFixture]
    public class GitHubApiCallTest : BaseServiceUnitTest
    {

        private MockRepository _mockRepository;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _mockRepository = new MockRepository(MockBehavior.Default);
            _mockHttpHandler = _mockRepository.Create<IHttpHandler>();
        }

        [Test(Description = "Check that the call to the git hub api returns a result as a string")]
        [Category("GitHubAPI")]
        [TestCase("robconery")]
        public void GitHub_API_CheckAPI_CheckForValidUser(string userName)
        {
            // Arrange
            BuildMockHeader(userName);

            ICallGitHubService callGitHubService = new CallGitHubService(this._mockHttpHandler.Object);
            string userUrl = string.Format(UsersUrl, userName);

            // Act
            Task<GitHubUserServiceModel> apiResponse = callGitHubService.CallUserApi(userUrl);

            apiResponse.Wait();

            // Assert
            Assert.IsNotEmpty(apiResponse.Result.name, "Response was empty");
        }



        [Test(Description = "Check that the call to the git hub api returns a result as a string")]
        [Category("GitHubAPI")]
        [TestCase("NotAValidUser")]
        public void GitHub_API_CheckAPI_CheckForInValidUser(string userName)
        {
            try
            {
                // Arrange
                _mockHttpHandler.Setup(c => c.HttpCallClient<GitHubUserServiceModel>(It.IsAny<string>())).ThrowsAsync(new HttpResponseException("Not Found"));

                ICallGitHubService callGitHubService = new CallGitHubService(this._mockHttpHandler.Object);

                string userUrl = string.Format(UsersUrl, userName);

                // Act
                Task<GitHubUserServiceModel> apiResponse = callGitHubService.CallUserApi(userUrl);
                apiResponse.Wait();
            }
            catch (Exception ex)
            {
                // Assert
                Assert.AreEqual(ex.InnerException.Message, "Not Found");
            }
        }

        [Test(Description = "Check that the call to the git hub api returns a result as a string")]
        [Category("GitHubAPI")]
        [TestCase("robconery")]
        public void GitHub_API_CheckAPI_CallReposURLWithResults(string userName)
        {
            // Arrange
            BuildMockHeader(userName);
            BuildMockReposLines();

            ICallGitHubService callGitHubService = new CallGitHubService(this._mockHttpHandler.Object);

            string userUrl = string.Format(UsersUrl, userName);

            Task<GitHubUserServiceModel> apiResponse = callGitHubService.CallUserApi(userUrl);
            apiResponse.Wait();

            // Act
            Task<List<GitHubUserReposServiceModelItem>> apiReposResponse = callGitHubService.CallUserReposApi(apiResponse.Result.repos_url);
            apiReposResponse.Wait();

            // Assert
            Assert.IsTrue(apiReposResponse.Result.Count > 0, "Response was empty");
        }


        [Test(Description = "Check that the call to the git hub api returns null for a valid repos")]
        [Category("GitHubAPI")]
        [TestCase("speacock1970")]
        public void GitHub_API_CheckAPI_CallUserReposAPIURLWithNoRepositories(string userName)
        {
            // Arrange
            BuildMockReposNoLines();
            ICallGitHubService callGitHubService = new CallGitHubService(this._mockHttpHandler.Object);
            string userUrl = string.Format(UsersUrl, userName) + "/repos";

            // Act
            // Assert
            Assert.IsNull(callGitHubService.CallUserReposApi(userUrl).GetAwaiter().GetResult());
        }
    }
}