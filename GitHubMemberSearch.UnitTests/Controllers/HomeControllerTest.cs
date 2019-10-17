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

namespace GitHubMemberSearch.UnitTests.Controllers
{
    [TestFixture]
    public class HomeControllerTest : BaseServiceUnitTest
    {
        [SetUp]
        public void Initialize()
        {
            AutoMapperConfig.CreateMappings();
        }

        [Test(Description = "Verify that the result return contains a value for a valid user name")]
        [Category("HomeControllerTest")]
        [TestCase("robconery")]
        public void Controller_CheckController_ValidateResultIsReturned(string userName)
        {
            //Arrange
            var controller = new HomeController(new CallGitHubService());
            //Act
            var result = controller.Search(userName) as Task<ActionResult>;
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
            var controller = new HomeController(new CallGitHubService());
            //Act
            Task<ActionResult> result = controller.Search(userName) as Task<ActionResult>;
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
            var controller = new HomeController(new CallGitHubService());
            //Act
            Task<ActionResult> result = controller.Search(userName) as Task<ActionResult>;
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
            var controller = new HomeController(new CallGitHubService());
            //Act
            Task<ActionResult> result = controller.Search(userName) as Task<ActionResult>;
            result.Wait();
            ViewResult viewResult = result.Result as ViewResult;
            GitHubUserViewSearchModel model = viewResult.Model as GitHubUserViewSearchModel;
            //Assert
            Assert.IsTrue(model.UserViewModel[0].reposItems.Count > 0);
        }
    }
}