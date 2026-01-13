using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;

namespace SeleniumLearning.TestScripts
{
    public class givetest
    {
        IWebDriver driver;

        [SetUp]
        public void Startbrowser()
        {
            new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
            driver = new FirefoxDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Url = "https://testautomationpractice.blogspot.com/";
            Assert.AreEqual("https://testautomationpractice.blogspot.com/", driver.Url);
        }

        [Test]
        public void Slidebarcheck()
        {
            IWebElement slidertitle = driver.FindElement(By.XPath("//h2[text()='Slider']"));

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView();", slidertitle);
            Thread.Sleep(2000);

            IWebElement slide2 = driver.FindElement(By.XPath("//div[@id='slider-range']/span[2]"));

            Actions sliderhandle = new Actions(driver);
            sliderhandle.ClickAndHold(slide2).MoveByOffset(-30,0).Perform();
            Thread.Sleep(2000);

            sliderhandle.ClickAndHold(slide2).MoveByOffset(50,0).Perform();


        }

        [TearDown]
        public void Closebrowser()
        {
            Thread.Sleep(5000);
            driver.Dispose();
        }

    }
}
