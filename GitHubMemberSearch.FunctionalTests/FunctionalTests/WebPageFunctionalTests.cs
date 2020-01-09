using NUnit.Framework;
using OpenQA.Selenium;
using System.Diagnostics.CodeAnalysis;

namespace GitMemberSearch.FunctionalTests
{
    [ExcludeFromCodeCoverage]

    [TestFixture]
    public class WebPageFunctionalTests:BaseFunctionalTests
    {
        [Test]
        public void WebPage_EmptyUserName_Check_Field_Error_Is_Shown()
        {
            SearchField.SendKeys("");
            SearchButton.Click();
            Assert.IsNotNull(ErrorLabel);
        }

        [Test]
        public void WebPage_EmptyUserName_Check_Field_Error_Is_NotShown()
        {
            SearchField.SendKeys("ascrees");
            SearchButton.Click();
            Assert.IsTrue(driver.FindElements(By.Id("userName-error")).Count == 0);
        }

        [Test]
        public void WebPage_EmptyUserName_Check_Search_Error_Is_Shown()
        {
            SearchField.SendKeys("...");
            SearchButton.Click();
            Assert.IsTrue(SearchError.Text.Contains("No records found for user"));
        }


        [Test]
        public void WebPage_EmptyUserName_Check_Field_Name_Is_Shown_for_Valid_User()
        {
            SearchField.SendKeys("ascrees");
            SearchButton.Click();
            Assert.IsTrue(NameField.Text.Contains("Name"));
        }

        [Test]
        public void WebPage_EmptyUserName_Check_No_Repository_Items_Valid_User()
        {
            SearchField.SendKeys("speacock1970");
            SearchButton.Click();
            Assert.IsTrue(NoRepositoryItems.Text.Contains("The user does not have any repository items"));
        }

        [Test]
        public void WebPage_EmptyUserName_Check_No_Repository_Items_Not_Shown_For_Valid_User()
        {
            SearchField.SendKeys("ascrees");
            SearchButton.Click();
            Assert.IsTrue(!NoRepositoryItems.Text.Contains("The user does not have any repository items"));
        }

    }
}
