using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
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
        public void TestMethod_1()
        {
            driver.Url = "https://testautomationpractice.blogspot.com/";
            Assert.AreEqual("https://testautomationpractice.blogspot.com/", driver.Url);
            Console.WriteLine("URL Matches Today!!!");

            //Name field identification, validating that it's empty and filling
            IWebElement Name = driver.FindElement(By.CssSelector("#name"));
            Assert.IsEmpty(Name.Text);
            Name.SendKeys(Keys.Enter);
            Thread.Sleep(2000);
            Name.SendKeys("Priya");
            Name.SendKeys(Keys.Tab);
            Thread.Sleep(2000);

            //Email field identification, validating that it's empty and adding value
            IWebElement Email = driver.FindElement(By.XPath("//input[@placeholder='Enter EMail']"));
            Assert.IsEmpty(Email.Text);
            Email.SendKeys(Keys.Enter);
            Thread.Sleep(2000);
            Email.SendKeys("vinay@gmail.com");
            Email.SendKeys(Keys.Tab);

            //Selecting values from dropdown and validating nothing is selected before
            IWebElement colors_dropdown = driver.FindElement(By.XPath("//select[@id='colors']"));

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView();", colors_dropdown);
            Thread.Sleep(2000);

            js.ExecuteScript("arguments[0].scrollTop = arguments[0].scrollHeight", colors_dropdown);
            Thread.Sleep(2000);

            js.ExecuteScript("arguments[0].scrollTop =0", colors_dropdown);
            Thread.Sleep(2000);

            SelectElement select = new SelectElement(colors_dropdown);
            Assert.AreEqual(0, select.AllSelectedOptions.Count,"Dropdown has selected values!");
            select.DeselectAll();
            select.SelectByValue("white");
            Thread.Sleep(2000);
            select.SelectByText("Blue");
            Thread.Sleep(2000);
            select.SelectByIndex(3);
          
        }

        [TearDown]
        public void Closebrowser()
        {
            Thread.Sleep(10000);
            driver.Dispose();

        }

       
       
    }
}
