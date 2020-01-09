using NUnit.Framework;
using OpenQA.Selenium;
using System.Diagnostics.CodeAnalysis;
using System.Threading;

namespace GitMemberSearch.FunctionalTests
{
    [ExcludeFromCodeCoverage]

    [TestFixture]
    public class WebPageFunctionalTests:BaseFunctionalTestClass
    {
        [Test]
        public void WebPage_EmptyUserName_Check_Field_Error_Is_Shown()
        {
            IWebElement searchField = driver.FindElement(By.Id("userName"));
            IWebElement buttonField = driver.FindElement(By.Name("Search"));
            searchField.SendKeys("");
            buttonField.Click();
            IWebElement errorField = driver.FindElement(By.Id("userName-error"));
            Assert.IsNotNull(errorField);
        }

        [Test]
        public void WebPage_EmptyUserName_Check_Field_Error_Is_NotShown()
        {
            IWebElement searchField = driver.FindElement(By.Id("userName"));
            IWebElement buttonField = driver.FindElement(By.Name("Search"));
            searchField.SendKeys("arees");
            buttonField.Click();
            Assert.IsTrue(driver.FindElements(By.Id("userName-error")).Count == 0);
        }

        [Test]
        public void WebPage_EmptyUserName_Check_Search_Error_Is_Shown()
        {
            IWebElement searchField = driver.FindElement(By.Id("userName"));
            IWebElement buttonField = driver.FindElement(By.Name("Search"));
            searchField.SendKeys("...");
            buttonField.Click();
            IWebElement searchError = driver.FindElement(By.XPath("//*[@id='userDetailsTable']/tbody/tr/td"));
            Assert.IsTrue(searchError.Text.Contains("No records found for user"));
        }
    }
}
