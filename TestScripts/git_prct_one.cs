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
            driver.Url = "https://testautomationpractice.blogspot.com/";
            Assert.AreEqual("https://testautomationpractice.blogspot.com/", driver.Url);
            Console.WriteLine("URL Matches Today!!!");
        }

        [Test]
        public void TestMethod_1()
        {
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
            Thread.Sleep(2000);

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
            Thread.Sleep(2000);

            //Select a country from drop down
            IWebElement country = driver.FindElement(By.CssSelector("#country"));
            js.ExecuteScript("arguments[0].scrollIntoView(true);", country);
            Thread.Sleep(2000);

            SelectElement select_country = new SelectElement(country);
            select_country.SelectByText("India");
            Thread.Sleep(2000);

            //Select from scrolling dropdown
            IWebElement selectfromscroll = driver.FindElement(By.CssSelector("#comboBox"));

           

            js.ExecuteScript("arguments[0].scrollIntoView(true);", selectfromscroll);
            Thread.Sleep(2000);

            Assert.IsEmpty(selectfromscroll.Text);
            selectfromscroll.SendKeys(Keys.Enter);
            Thread.Sleep(2000);

            IWebElement x =  driver.FindElement(By.XPath("//div[@id='dropdown']/div[text()='Item 40']"));

            js.ExecuteScript("arguments[0].scrollIntoView(true);", x);
            Thread.Sleep(3000);
            x.Click();

        }

        [Test]
        public void Alertworks()
        {
            IWebElement simplealert = driver.FindElement(By.XPath("//button[text()='Simple Alert']"));

            simplealert.Click();
            
            WebDriverWait wait = new WebDriverWait(driver,TimeSpan.FromSeconds(10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.AlertIsPresent());

            driver.SwitchTo().Alert();
            Thread.Sleep(2000);
            driver.SwitchTo().Alert().Accept();
            Thread.Sleep(2000);

            IWebElement confirmalert = driver.FindElement(By.XPath("//button[@id='confirmBtn']"));
            confirmalert.Click();
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.AlertIsPresent());
            driver.SwitchTo().Alert();
            Thread.Sleep(2000);
            driver.SwitchTo().Alert().Dismiss();
            Thread.Sleep(2000);

            IWebElement promptalert = driver.FindElement(By.CssSelector("#promptBtn"));
            promptalert.Click();
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.AlertIsPresent());

            driver.SwitchTo().Alert();
            driver.SwitchTo().Alert().SendKeys("Lovely!!");
            Thread.Sleep(2000);
            driver.SwitchTo().Alert().Accept();

            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("demo")));
            string x = driver.FindElement(By.Id("demo")).Text;
            Console.WriteLine(x);

            
        }

        [Test, Category("one")]
        public void Actions_implementation()
        {
            
            IWebElement startbutton = driver.FindElement(By.Name("start"));

            WebDriverWait wait = new WebDriverWait(driver,TimeSpan.FromSeconds(10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Name("start")));

            string z = startbutton.GetCssValue("background-color");
            Console.WriteLine(z);

            Actions x = new Actions(driver);
            x.MoveToElement(startbutton).Click().Perform();
            
            x.MoveByOffset(-200,-200).Perform();
            //Thread.Sleep(1000);

            IWebElement stopbutton = driver.FindElement(By.Name("stop"));
            wait.Until(d =>
                            stopbutton.GetCssValue("background-color")
                            .Equals("rgb(255, 0, 0)")
                       );


            string y = stopbutton.GetCssValue("background-color");
            Console.WriteLine(y);

        }

        [Test, Category("two")]
        public void selectday()
        {
            string str = Myday();
            IWebElement day = driver.FindElement(By.XPath("//input[@id='"+str+"']"));

            Assert.IsFalse(day.Selected, "Checkbox should not be selected but selected");

            day.Click();
            Assert.IsTrue(day.Selected); 
           
        }

        public static string Myday()
        {
            string day = "tuesday";
            return day;
        }

        [Test]
        public void Newtab()
        {
            IWebElement btn = driver.FindElement(By.XPath("//button[text()='New Tab']"));
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView();", btn);

            Thread.Sleep(2000);
            btn.Click();
            Thread.Sleep(1000);

            string parentwindow = driver.WindowHandles[0];
            string childwindow = driver.WindowHandles[1];

            driver.SwitchTo().Window(childwindow);
            Thread.Sleep(2000);
            driver.SwitchTo().Window(parentwindow);


        }


        [Test]
        public void Date_Selection()
        {
            IWebElement date = driver.FindElement(By.XPath("//input[@id='datepicker']"));
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView();", date);

            //string before = date.Text;

            date.Click();
            date.SendKeys("20/12/2025");
            date.SendKeys(Keys.Enter);

            //string after = date.Text;

            Thread.Sleep(1000);

            IWebElement date2 = driver.FindElement(By.Id("txtDate"));
            js.ExecuteScript("document.getElementById('txtDate').value='2025-12-21';");
            Thread.Sleep(2000);

            js.ExecuteScript("document.getElementById('start-date').value='2025-12-22';");

        }

        [Test]
        public void Pointme()
        {
            IWebElement text = driver.FindElement(By.XPath("//h2[text()='Mouse Hover']"));
            IWebElement point = driver.FindElement(By.XPath("//button[text()='Point Me']"));

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView();", text);

            Thread.Sleep(2000);

            Actions x = new Actions(driver);
            x.MoveToElement(point).Build().Perform();
            Thread.Sleep(2000);

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//a[text()='Mobiles']")));

            IWebElement list1 = driver.FindElement(By.XPath("//a[text()='Mobiles']"));
            list1.Click();


        }

        [Test]
        public void Dreagndrop()
        {
            IWebElement dragndrop = driver.FindElement(By.XPath("//h2[text()='Drag and Drop']"));

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView();", dragndrop);

            Thread.Sleep(2000);

            IWebElement source = driver.FindElement(By.CssSelector("#draggable"));
            IWebElement destination = driver.FindElement(By.CssSelector("#droppable"));

            Actions x = new Actions(driver);
            x.DragAndDrop(source, destination).Perform();
        }

        [TearDown]
        public void Closebrowser()
        {
            Thread.Sleep(5000);
            driver.Dispose();

        }

       
       
    }
}
