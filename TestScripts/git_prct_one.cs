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

        [Test]
        public void doubleclickcheck()
        {
            IWebElement text = driver.FindElement(By.XPath("//h2[text()='Double Click']"));
            IWebElement text1 = driver.FindElement(By.XPath("//button[text()='Copy Text']"));
            IWebElement text2 = driver.FindElement(By.XPath("//input[@id='field2']"));
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView();", text);
            Thread.Sleep(2000);

            Actions k = new Actions(driver);
            k.MoveToElement(text1).DoubleClick().Build().Perform();
            Thread.Sleep(1000);

            string v = text2.GetAttribute("value");
            Console.WriteLine(v);

        }

        [Test]
        public void Uploadfiles()
        {
            //string file1 = "C:\\Users\\HP\\Desktop\\Upload 1.docx";
            string[] files =
            {
                "C:\\Users\\HP\\Desktop\\upload 2.docx",
                "C:\\Users\\HP\\Desktop\\Upload 1.docx"
            };

            IWebElement upload1 = driver.FindElement(By.Id("singleFileInput"));
            IWebElement upload2 = driver.FindElement(By.Id("multipleFilesInput"));
            IWebElement rolldown = driver.FindElement(By.XPath("//h2[text()='Upload Files']"));
            IWebElement upbtn1 = driver.FindElement(By.XPath("//button[text()='Upload Multiple Files']"));

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView();", rolldown);
            Thread.Sleep(2000);

            foreach (var file in files)
            {
                upload2.SendKeys(file);
            }
            upbtn1.Click();



        }

        [Test]
        public void Slidebar()
        {
            IWebElement slidertitle = driver.FindElement(By.XPath("//h2[text()='Slider']"));
            IWebElement slider = driver.FindElement(By.XPath("//div[@id='slider-range']/span[2]"));

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView({behavior:'smooth'});", slidertitle);
            Thread.Sleep(2000);

            Actions x = new Actions(driver);
            x.ClickAndHold(slider).MoveByOffset(50, 0).Release().Perform();
            x.ClickAndHold(slider).MoveByOffset(-30, 0).Release().Perform();
        }

        [Test]
        public void Pagination_and_recordselect()
        {
            IWebElement title11 = driver.FindElement(By.XPath("//h2[text()='Pagination Web Table']"));

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView({behavior:'smooth'});", title11);
            //Thread.Sleep(3000);

            ////table[@id='productTable']/tbody/tr
            IList<IWebElement> pages = driver.FindElements(By.XPath("//ul[@id='pagination']//a"));
            int pagecount = driver.FindElements(By.XPath("//ul[@id='pagination']//a")).Count;

            for (int i = 0; i < pages.Count; i++)
            {
                pages[i].Click();

                IList<IWebElement> checkboxs = driver.FindElements(By.XPath("//table[@id='productTable']/tbody/tr/td[4]/input"));
                foreach (var x in checkboxs)
                {
                    x.Click();
                    Thread.Sleep(1000);
                }

                Thread.Sleep(1000);

            }


        }

        [TearDown]
        public void Closebrowser()
        {
            Thread.Sleep(5000);
            driver.Dispose();

        }

       
       
    }
}
