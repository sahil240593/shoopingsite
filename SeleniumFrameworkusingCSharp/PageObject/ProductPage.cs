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
    public class ProductPage
    {
        private IWebDriver driver;
        By cardTitle = By.CssSelector(".card-title a");
        By addToCartButton = By.CssSelector(".card-footer button");
        public ProductPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }
        [FindsBy(How = How.TagName, Using = "app-card")]
        private IList <IWebElement> cards;
        [FindsBy(How = How.PartialLinkText, Using = "Checkout")]
        private IWebElement checkout;

        public void waitForPage()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.PartialLinkText("Checkout")));
        }

        public IList<IWebElement> getCards()
        {
            return cards;   
        }
        public CheckoutPage checkoutButton()
        {
             checkout.Click();
            return new CheckoutPage(driver);
           
            
        }
        public By getCardTitle()
        {
            return cardTitle;   
        }
        public By addToCart()
        {
            return addToCartButton;
        }

    }
}
