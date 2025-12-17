using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumLearning.Pageobjects
{
    public class radiobutton
    {
        IWebDriver driver;
        public radiobutton(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(this.driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//input[@name='gender']")]
        IList<IWebElement> radios;

        public void Selectradiobutton()
        {
            foreach(var x in radios)
            {
                if (x.GetAttribute("value") == "male")
                   x.Click();
                Assert.IsTrue(x.Selected, "Radio button should be selected!");
                break;
                
            }
        }
    }
}
