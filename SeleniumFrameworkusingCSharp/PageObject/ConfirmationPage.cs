using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumFrameworkusingCSharp.PageObject
{
    public class ConfirmationPage
        //driver.FindElement(By.XPath("//label[@for='checkbox2']")).Click();
           // driver.FindElement(By.XPath("//input[@value='Purchase']")).Click();
    {
        private IWebDriver driver;
        public ConfirmationPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);

        }
        [FindsBy(How = How.Id, Using = "country")]
        private IWebElement country;
        [FindsBy(How = How.XPath, Using = "//label[@for='checkbox2']")]
        private IWebElement checkboxsecond;
        [FindsBy(How = How.XPath, Using = "//input[@value='Purchase']")]
        private IWebElement purchasebutton;
        public void CountryTextbox(string countryselection)
        {
            country.SendKeys(countryselection); 
        }
        public void waitForPage()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.LinkText("India"))).Click();
        }
        public void CheckboxHandle()
        {
            checkboxsecond.Click();
        }
        public void PurchaseButton()
        {
            purchasebutton.Click(); 
        }
    }
}
