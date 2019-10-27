using GitHubMemberSearch.Service.Helper;
using GitHubMemberSearch.Services;
using GitHubMemberSearch.Services.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace GitHubMemberSearch.UnitTests.Services
{
    [ExcludeFromCodeCoverage]

    [TestFixture]
    public class HTTPHandlerTest : BaseServiceUnitTest
    {
        [Test(Description = "Check that the httphandler initializes")]
        [Category("HttpHandler")]
        public void HttpHandler_CheckHandler_VerifyUserAgent()
        {
            //Arrange

            //Act
            HttpHandler.InitializeClient();

            //Assert
            Assert.IsTrue(HttpHandler.ApiClient.DefaultRequestHeaders.Contains("User-Agent"), "User Agent Is Missing");
        }

        [Test(Description = "Check that the httphandler throws an exception when nothing is returned")]
        [Category("HttpHandler")]
        public void HttpHandler_CheckHandler_VerifyExceptionRaised()
        {
            try
            {
                //Arrange
                string urlToTest = "https://api.github.com/users/NowtTofind";
                HttpHandler.InitializeClient();

                //Act

                Task<GitHubUserServiceModel> apiResponse = HttpHandler.HTTPCallClient<GitHubUserServiceModel>(urlToTest);
                apiResponse.Wait();
            }
            catch (Exception ex)
            {
                //Assert
                Assert.AreEqual(ex.InnerException.Message, "Not Found");
            }
        }

        [Test(Description = "Check that the httphandler returns valid values")]
        [Category("HttpHandler")]
        public void HttpHandler_CheckHandler_VerifyValidURLReturnsUser()
        {
            try
            {
                //Arrange
                string urlToTest = "https://api.github.com/users/ASCRees";
                HttpHandler.InitializeClient();

                //Act

                Task<GitHubUserServiceModel> apiResponse = HttpHandler.HTTPCallClient<GitHubUserServiceModel>(urlToTest);
                apiResponse.Wait();

                //Assert
                Assert.IsTrue(apiResponse.Result.id > 0, "Response was empty");
            }
            catch (Exception ex)
            {
            }
        }
    }
}