using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using GitHubMemberSearch.Service.Helper;
using GitHubMemberSearch.Service.Interfaces;
using GitHubMemberSearch.Services.Models;
using Moq;
using NUnit.Framework;

namespace GitHubMemberSearch.UnitTests.Services
{
    [ExcludeFromCodeCoverage]

    [TestFixture]
    public class HttpHandlerTest : BaseServiceUnitTest
    {
        IHttpHandler _httpHandler;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _httpHandler = new HttpHandler();
        }


        [Test(Description = "Check that the httphandler initializes")]
        [Category("HttpHandler")]
        public void HttpHandler_CheckHandler_VerifyUserAgent()
        {
            // Arrange

            // Act
            _httpHandler.InitializeClient();

            // Assert
            Assert.IsTrue(_httpHandler.ApiClient.DefaultRequestHeaders.Contains("User-Agent"), "User Agent Is Missing");
        }

        [Test(Description = "Check that the httphandler throws an exception when nothing is returned")]
        [Category("HttpHandler")]
        public void HttpHandler_CheckHandler_VerifyExceptionRaised()
        {
            try
            {
                // Arrange
                string urlToTest = "https://api.github.com/users/NowtTofind";
                _httpHandler.InitializeClient();

                // Act
                Task<GitHubUserServiceModel> apiResponse = _httpHandler.HttpCallClient<GitHubUserServiceModel>(urlToTest);
                apiResponse.Wait();
            }
            catch (Exception ex)
            {
                // Assert
                Assert.AreEqual(ex.InnerException.Message, "Not Found");
            }
        }

        [Test(Description = "Check that the httphandler returns valid values")]
        [Category("HttpHandler")]
        public void HttpHandler_CheckHandler_VerifyValidURLReturnsUser()
        {
            // Arrange
            string urlToTest = "https://api.github.com/users/ASCRees";
            _httpHandler.InitializeClient();

            // Act
            Task<GitHubUserServiceModel> apiResponse = _httpHandler.HttpCallClient<GitHubUserServiceModel>(urlToTest);
            apiResponse.Wait();

            // Assert
            Assert.IsTrue(apiResponse.Result.Id > 0, "Response was empty");
        }


        //public void HttpHandler_CheckHandler_Exception_For_403()
        //{
        //    var httpClientMock = new Mock<IHttpHandler>();
        //    httpClientMock.Setup(c => c.GetAsync<SomeModelObject>(It.IsAny<string>()))
        //        .Returns(() => Task.FromResult(model));
        //}
    }
}