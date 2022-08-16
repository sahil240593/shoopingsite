using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;

namespace SeleniumFundamental
{
    internal class SortWebTables
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
            driver.Url = "https://rahulshettyacademy.com/seleniumPractise/#/offers";

        }
        [Test]
        public void sortTable()
        {
            ArrayList arrayA = new ArrayList();
            SelectElement dropdown = new SelectElement(driver.FindElement(By.Id("page-menu")));
            dropdown.SelectByValue("20");
            //Get all veggie in the array list a
            IList <IWebElement> Veggies = driver.FindElements(By.XPath("//tr/td[1]"));
            foreach (IWebElement element in Veggies)
            {
                arrayA.Add(element.Text);
                
            }
           

            //Sort the arraylist 
            foreach(string elements in arrayA)
            {
                TestContext.Progress.WriteLine(elements);
            }
            arrayA.Sort();   
            foreach (string elements in arrayA)
            {
                TestContext.Progress.WriteLine(elements);
            }
            //Go and Click column
            driver.FindElement(By.CssSelector("th[aria-label*='fruit name:']")).Click();
            //Get all the veggies in array list b
            ArrayList arrayB = new ArrayList();
            IList<IWebElement> SortedVeggies = driver.FindElements(By.XPath("//tr/td[1]"));
            foreach (IWebElement element in SortedVeggies)
            {
                arrayB.Add(element.Text);

            }

            //array A to B equal
            Assert.AreEqual(arrayA,arrayB);

        }
    }
}
