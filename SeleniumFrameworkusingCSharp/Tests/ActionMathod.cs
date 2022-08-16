using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;

namespace SeleniumFundamental
{
    internal class ActionMathod
    {
        IWebDriver driver;
        [SetUp]
        public void StartBrowser()
        {

            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver();

            driver.Manage().Window.Maximize();
            //Implicit wait give for 5 second globally
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Url = "https://rahulshettyacademy.com/";

    
        }
        [Test]
        public void actionMathod()
        {
            Actions action = new Actions(driver);

            action.MoveToElement(driver.FindElement(By.CssSelector("div[class='nav-outer clearfix'] a[class='dropdown-toggle']"))).Perform();

            driver.FindElement(By.XPath("//ul[@class='dropdown-menu']/li[1]/a")).Click();
            
        }

    }

}
