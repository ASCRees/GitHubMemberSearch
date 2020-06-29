using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using System.Web.Mvc;
using GitHubMemberSearch.App_Start;
using GitHubMemberSearch.Controllers;
using GitHubMemberSearch.Models;
using GitHubMemberSearch.Service.Helper;
using GitHubMemberSearch.Service.Interfaces;
using GitHubMemberSearch.Services;
using Moq;
using NUnit.Framework;

namespace GitHubMemberSearch.UnitTests.Controllers
{
    [ExcludeFromCodeCoverage]

    [TestFixture]
    public class HomeControllerTest : BaseServiceUnitTest
    {
        private MockRepository _mockRepository;
        private Mock<ICallGitHubService> _mockCallGitHubService;
        private Mock<IHomeModelBuilder> _mockHomeModelBuilder;

        [SetUp]
        public void SetUp()
        {
            this._mockRepository = new MockRepository(MockBehavior.Default);
            this._mockHttpHandler = this._mockRepository.Create<IHttpHandler>();
            this._mockCallGitHubService = this._mockRepository.Create<ICallGitHubService>();
            this._mockHomeModelBuilder = this._mockRepository.Create<IHomeModelBuilder>();
            AutoMapperConfig.CreateMappings();
        }

        [Test(Description = "Verify that the result return contains result")]
        [Category("HomeControllerTest")]
        [TestCase("robconery")]
        public void Controller_CheckController_ValidateResultIsReturned(string userName)
        {
            // Arrange
            var controller = new HomeController(new CallGitHubService(_mockHttpHandler.Object), new HomeModelBuilder());

            // Act
            var result = controller.Search(userName);
            result.Wait();

            // Assert
            Assert.NotNull(result.Result);
        }

        [Test(Description = "Verify that the model has an id for a valid user name")]
        [Category("HomeControllerTest")]
        [TestCase("robconery")]
        public void Controller_CheckController_ValidUserName(string userName)
        {

            // Arrange
            BuildMockHeader(userName);
            BuildMockReposLines();

            var controller = new HomeController(new CallGitHubService(_mockHttpHandler.Object), new HomeModelBuilder());

            // Act
            Task<ActionResult> result = controller.Search(userName);
            result.Wait();
            ViewResult viewResult = result.Result as ViewResult;
            GitHubUserViewSearchModel model = viewResult.Model as GitHubUserViewSearchModel;

            // Assert
            Assert.IsTrue(model.UserViewModel[0].id > 0);
        }

        [Test(Description = "Verify that the model has an id of 0 for an invalid user name")]
        [Category("HomeControllerTest")]
        [TestCase("NothingIsFound")]
        public void Controller_CheckController_InValidUserName(string userName)
        {
            // Arrange
            var controller = new HomeController(new CallGitHubService(_mockHttpHandler.Object), new HomeModelBuilder());

            // Act
            var result = controller.Search(userName);
            result.Wait();
            ViewResult viewResult = result.Result as ViewResult;
            GitHubUserViewSearchModel model = viewResult.Model as GitHubUserViewSearchModel;

            // Assert
            Assert.IsTrue(model.UserViewModel[0].id.Equals(0));
        }

        [Test(Description = "Verify that the model has repository items for a user")]
        [Category("HomeControllerTest")]
        [TestCase("robconery")]
        public void Controller_CheckController_ReposItems(string userName)
        {
            // Arrange
            BuildMockHeader(userName);
            BuildMockReposLines();

            var controller = new HomeController(new CallGitHubService(_mockHttpHandler.Object), new HomeModelBuilder());

            // Act
            var result = controller.Search(userName);
            result.Wait();
            ViewResult viewResult = result.Result as ViewResult;
            GitHubUserViewSearchModel model = viewResult.Model as GitHubUserViewSearchModel;

            // Assert
            Assert.IsTrue(model.UserViewModel[0].reposItems.Count > 0);
        }

        [Test]
        public void Index_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var homeController = this.CreateHomeController();

            // Act
            var result = homeController.Index(string.Empty);

            // Assert
            Assert.IsTrue(result != null);
        }

        [Test]
        public async Task Search_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var homeController = this.CreateHomeController();

            // Act
            var result = await homeController.Search(string.Empty);

            // Assert
            Assert.IsTrue(result != null);
        }

        [Test(Description = "Verify that the model builder returns no records return")]
        [Category("HomeModelBuilder")]
        [TestCase("ASCREES222")]
        [TestCase("ASCREES221")]
        public void ModelBuilder_CheckController_CheckSearchRedirectsToError(string userName)
        {
            // Arrange
            this._mockHomeModelBuilder.Setup(x => x.BuildSearchViewModel(It.IsAny<string>()))
                .Throws(new ArgumentException("Mock Exception"));

            var controller = this.CreateHomeController();

            // Act
            var result = controller.Search(userName);
            result.Wait();
            var viewResult = result.Result;

            // Assert
            Assert.IsTrue(viewResult.GetType().Equals(typeof(RedirectResult)));
        }

        [Test(Description = "Verify that the model builder returns no records return")]
        [Category("HomeModelBuilder")]
        [TestCase("ASCREES222")]
        public void ModelBuilder_CheckBuilder_ReturnsNoRecordFound(string userName)
        {
            // Arrange
            var builder = new HomeModelBuilder
            {
                HomeControllerObj = this.CreateHomeController(),
            };

            // Act
            var builderResult = builder.BuildSearchViewModel(userName).GetAwaiter().GetResult();

            // Assert
            Assert.IsTrue(builderResult.message.Contains("No records found"));
        }

        private HomeController CreateHomeController()
        {
            return new HomeController(
                this._mockCallGitHubService.Object,
                this._mockHomeModelBuilder.Object);
        }
    }
}