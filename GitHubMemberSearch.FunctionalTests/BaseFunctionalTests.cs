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

    public class BaseFunctionalTests
    {
        public IWebDriver driver = new ChromeDriver();

        public IWebElement SearchField 
        { 
            get 
            {
                return driver.FindElement(By.Id("userName"));
            } 
        }

        public IWebElement SearchButton
        {
            get
            {
                return driver.FindElement(By.Name("Search"));
            }
        }

        public IWebElement ErrorLabel
        {
            get
            {
                return driver.FindElement(By.Id("userName-error"));
            }
        }

        public IWebElement NameField
        {
            get
            {
                return driver.FindElement(By.XPath("//*[@id='userDetailsTable']/tbody/tr[1]/th"));
            }
        }

        public IWebElement SearchError
        {
            get
            {
                return driver.FindElement(By.XPath("//*[@id='userDetailsTable']/tbody/tr/td"));
            }
        }

        public IWebElement NoRepositoryItems
        {
            get
            {
                return driver.FindElement(By.XPath("/html/body/div[2]/table[2]/tbody/tr/td"));
            }
        }

        public IWebElement ErrorPageText
        {
            get
            {
                return driver.FindElement(By.XPath("/html/body/div[2]/hgroup/h1"));
            }
        }
        
        [OneTimeSetUp]
        public void Open()
        {
            driver.Manage().Window.Maximize();
        }


        [SetUp]
        public void ReloadPage()
        {
            driver.Url = "http://localhost/GitSearch";
        }

        [OneTimeTearDown]
        public void Close()
        {
            driver.Close();
            driver.Dispose();
        }
    }
}
