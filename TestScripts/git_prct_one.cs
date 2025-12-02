using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;

namespace SeleniumLearning.TestScripts
{
    public class git_prct_one
    {
        IWebDriver driver;

        [SetUp]
        public void Startbrowser()
        {
            new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
            driver = new FirefoxDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
           
        }

        [Test]
        public void TestMethod1()
        {
            driver.Url = "https://testautomationpractice.blogspot.com";
            string E_Url = "https://testautomationpractice.blogspot.com/";
            Assert.AreEqual(E_Url, driver.Url);
            Console.WriteLine("Hurrey Url Matches!!!");

        }

        [TearDown]
        public void Closebrowser()
        {
            driver.Dispose();
        }

       
       
    }
}
