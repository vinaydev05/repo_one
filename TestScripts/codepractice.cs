using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using SeleniumLearning.Pageobjects;
using SeleniumLearning.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;

namespace SeleniumLearning.TestScripts
{
    public class codepractice:baseclass
    {
        [Test]
        public void Radiobutton()
        {
            radiobutton obj1 = new radiobutton(driver);
            obj1.Selectradiobutton();
        }
       
    }
}
