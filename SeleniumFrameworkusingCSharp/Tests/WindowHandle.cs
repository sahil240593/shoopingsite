using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;

namespace SeleniumFundamental
{
    internal class WindowHandle
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
            driver.Url = "https://rahulshettyacademy.com/loginpagePractise/";

        }
        [Test]
        public void childWindowHandle()
        {
            string email = "mentor@rahulshettyacademy.com";
            string parentWindow = driver.CurrentWindowHandle;
            driver.FindElement(By.ClassName("blinkingText")).Click();
            Assert.AreEqual(2,driver.WindowHandles.Count); 
            string childWindow = driver.WindowHandles[1];
            driver.SwitchTo().Window(childWindow);
            TestContext.Progress.WriteLine(driver.FindElement(By.ClassName("red")).Text);
            string readText = (driver.FindElement(By.ClassName("red")).Text);
            //Please email us at mentor@rahulshettyacademy.com with below template to receive response
            //nowi split the text using split function and id is in first index 
            string[] splitText =  readText.Split("at");
            // now trim the text from front and back 
            string[] trimmedText = splitText[1].Trim().Split(" ");
            Assert.AreEqual(email, trimmedText[0]);
            driver.SwitchTo().Window(parentWindow);
            driver.FindElement(By.Id("username")).SendKeys(trimmedText[0]);
        }
    }
}
