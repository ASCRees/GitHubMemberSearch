using GitHubMemberSearch.App_Start;
using GitHubMemberSearch.Controllers;
using GitHubMemberSearch.Models;
using GitHubMemberSearch.Service.Helper;
using GitHubMemberSearch.Services;
using GitHubMemberSearch.Services.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using Moq;

namespace GitHubMemberSearch.UnitTests.Controllers
{
    [TestFixture]
    public class HomeControllerTest : BaseServiceUnitTest
    {
        private MockRepository mockRepository;

        private Mock<ICallGitHubService> mockCallGitHubService;
        private Mock<IHomeModelBuilder> mockHomeModelBuilder;

        [SetUp]
        public void SetUp()
        {
            this.mockRepository = new MockRepository(MockBehavior.Default);

            this.mockCallGitHubService = this.mockRepository.Create<ICallGitHubService>();
            this.mockHomeModelBuilder = this.mockRepository.Create<IHomeModelBuilder>();
            AutoMapperConfig.CreateMappings();
        }

        [TearDown]
        public void TearDown()
        {
            this.mockRepository.VerifyAll();
        }

        private HomeController CreateHomeController()
        {
            return new HomeController(
                this.mockCallGitHubService.Object,
                this.mockHomeModelBuilder.Object);
        }

        [Test(Description = "Verify that the result return contains a value for a valid user name")]
        [Category("HomeControllerTest")]
        [TestCase("robconery")]
        public void Controller_CheckController_ValidateResultIsReturned(string userName)
        {
            //Arrange
            var controller = new HomeController(new CallGitHubService(), new HomeModelBuilder());
            //Act
            var result = controller.Search(userName);
            result.Wait();
            //Assert
            Assert.NotNull(result);
        }

        [Test(Description = "Verify that the model has an id for a valid user name")]
        [Category("HomeControllerTest")]
        [TestCase("robconery")]
        public void Controller_CheckController_ValidUserName(string userName)
        {
            //Arrange
            var controller = new HomeController(new CallGitHubService(), new HomeModelBuilder());
            //Act
            Task<ActionResult> result = controller.Search(userName);
            result.Wait();
            ViewResult viewResult = result.Result as ViewResult;
            GitHubUserViewSearchModel model = viewResult.Model as GitHubUserViewSearchModel;
            //Assert
            Assert.IsTrue(model.UserViewModel[0].id > 0);
        }

        [Test(Description = "Verify that the model has an id of 0 for an invalid user name")]
        [Category("HomeControllerTest")]
        [TestCase("NothingIsFound")]
        public void Controller_CheckController_InValidUserName(string userName)
        {
            //Arrange
            var controller = new HomeController(new CallGitHubService(), new HomeModelBuilder());
            //Act
            var result = controller.Search(userName);
            result.Wait();
            ViewResult viewResult = result.Result as ViewResult;
            GitHubUserViewSearchModel model = viewResult.Model as GitHubUserViewSearchModel;
            //Assert
            Assert.IsTrue(model.UserViewModel[0].id.Equals(0));
        }

        [Test(Description = "Verify that the model has repository items for a user")]
        [Category("HomeControllerTest")]
        [TestCase("robconery")]
        public void Controller_CheckController_ReposItems(string userName)
        {
            //Arrange
            var controller = new HomeController(new CallGitHubService(), new HomeModelBuilder());
            //Act
            var result = controller.Search(userName);
            result.Wait();
            ViewResult viewResult = result.Result as ViewResult;
            GitHubUserViewSearchModel model = viewResult.Model as GitHubUserViewSearchModel;
            //Assert
            Assert.IsTrue(model.UserViewModel[0].reposItems.Count > 0);
        }

        [Test]
        public void Index_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var homeController = this.CreateHomeController();
            string UserNameSearch = null;

            // Act
            var result = homeController.Index(
                UserNameSearch);

            // Assert
            Assert.IsTrue(result != null);
        }

        [Test]
        public async Task Search_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var homeController = this.CreateHomeController();
            string UserNameSearch = null;

            // Act
            var result = await homeController.Search(
                UserNameSearch);

            // Assert
            Assert.IsTrue(result!=null);
        }

        [Test(Description = "Verify that the model builder returns no records return")]
        [Category("HomeModelBuilder")]
        [TestCase("ASCREES222")]
        public void ModelBuilder_CheckController_CheckSearchRedirectsToError(string userName)
        {
            //Arrange
            this.mockHomeModelBuilder.Setup(x => x.BuildSearchViewModel(It.IsAny<string>()))
                .Throws(new ArgumentException("Mock Exception"));

            var controller = this.CreateHomeController();
 
            //Act
            var result = controller.Search(userName);
            result.Wait();
            var viewResult = result.Result;

            //Assert
            Assert.IsTrue(viewResult.GetType().Equals(typeof(System.Web.Mvc.RedirectResult)));
        }

        [Test(Description = "Verify that the model builder returns no records return")]
        [Category("HomeModelBuilder")]
        [TestCase("ASCREES222")]
        public void ModelBuilder_CheckBuilder_ReturnsNoRecordFound(string userName)
        {
            //Arrange

            var builder = new HomeModelBuilder();
            builder.HomeControllerObj = CreateHomeController();

            //Act
            var builderResult = builder.BuildSearchViewModel(userName).GetAwaiter().GetResult();

            //Assert
            Assert.IsTrue(builderResult.message.Contains(("No records found")));
        }
    }
}