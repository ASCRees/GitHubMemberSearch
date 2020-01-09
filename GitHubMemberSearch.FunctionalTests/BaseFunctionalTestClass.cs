using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace GitMemberSearch.FunctionalTests
{
    [ExcludeFromCodeCoverage]

    public class BaseFunctionalTestClass
    {
        public IWebDriver driver;

        [SetUp]
        public void Open()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Url = "http://localhost/GitSearch";
        }

        [TearDown]
        public void Close()
        {
            driver.Close();
            driver.Dispose();

        }
    }
}
