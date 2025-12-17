using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;

namespace SeleniumLearning.Utilities
{
    public class baseclass
    {
        public IWebDriver driver;

        [SetUp]
        public void Startbrowser()
        {
            new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
            driver = new FirefoxDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Url = "https://testautomationpractice.blogspot.com/";
           
        }


        [TearDown]
        public void CloseBrowser()
        {
            Thread.Sleep(5000);
            driver.Dispose();
        }
    }
}
