using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumFrameworkusingCSharp.PageObject;
using SeleniumFrameworkusingCSharp.Utilites;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using WebDriverManager.DriverConfigs.Impl;

namespace SeleniumFrameworkusingCSharp
{
    [Parallelizable(ParallelScope.Children)]
    public class End2Endtest : BaseClass
    {
        //Run all data sets of the test mathod in parallel
        //Run the test mathods in one class parallel
        //Run all test file in project parallel
        //dotnet test pathto.csproj ( ALl tests)
        //dotnet test pathto.csproj --filter TestCategory=Smoke

        // dotnet test CSharpSelFramework.csproj --filter TestCategory=Smoke -- TestRunParameters.Parameter\(name=\"browserName\",value=\"Chrome\"\)


        [Test,TestCaseSource("addTestDataConfig"),Category("Regression")]
        
       
        public void Homepagetestcase(string username, string password,string[] expectedProducts)
        {
            //string[] expectedProducts = { "iphone X", "Nokia Edge" };
            string[] actualProducts = new string[2];
            LoginPage loginpage = new LoginPage(getDriver());
            loginpage.validLogin(username, password);
            ProductPage productpage = new ProductPage(getDriver());
            productpage.waitForPage();
            IList<IWebElement> allProducts = productpage.getCards();

            foreach (IWebElement product in allProducts)
            {
                if (expectedProducts.Contains(product.FindElement(productpage.getCardTitle()).Text))
                {
                    product.FindElement(productpage.addToCart()).Click();

                }
                //string result = product.FindElement(By.CssSelector(".card-title a")).Text;
                //TestContext.Progress.WriteLine(result);

            }

            CheckoutPage checkoutpage = productpage.checkoutButton();
            IList<IWebElement> checkoutProducts = checkoutpage.CheckoutProducts();
            for (int i = 0; i < checkoutProducts.Count; i++)
            {
                actualProducts[i] = checkoutProducts[i].Text;

            }
            Assert.AreEqual(expectedProducts, actualProducts);
            ConfirmationPage confirmationpage = checkoutpage.Checkout();
            confirmationpage.CountryTextbox("India");
            confirmationpage.waitForPage();
            confirmationpage.CheckboxHandle();
            confirmationpage.PurchaseButton();

            string actualMessage = driver.Value.FindElement(By.XPath("//div[@class='alert alert-success alert-dismissible']")).Text;
            StringAssert.Contains("Success", actualMessage);
            TestContext.Progress.WriteLine(actualMessage);


        }
        public static IEnumerable<TestCaseData> addTestDataConfig()
        {
          yield return new TestCaseData(getDataParser().extractData("username"), getDataParser().extractData("password"),getDataParser().extractDataArray("product"));
          yield return new TestCaseData(getDataParser().extractData("username_wrong"), getDataParser().extractData("password_wrong"), getDataParser().extractDataArray("product"));
          yield return new TestCaseData(getDataParser().extractData("username"), getDataParser().extractData("password"), getDataParser().extractDataArray("product"));
          
        }

        [Test,Category("Smoke")]
        public void childWindowHandle()
        {
            string email = "mentor@rahulshettyacademy.com";
            string parentWindow = driver.Value.CurrentWindowHandle;
            driver.Value.FindElement(By.ClassName("blinkingText")).Click();
            Assert.AreEqual(2, driver.Value.WindowHandles.Count);
            string childWindow = driver.Value.WindowHandles[1];
            driver.Value.SwitchTo().Window(childWindow);
            TestContext.Progress.WriteLine(driver.Value.FindElement(By.ClassName("red")).Text);
            string readText = (driver.Value.FindElement(By.ClassName("red")).Text);
            //Please email us at mentor@rahulshettyacademy.com with below template to receive response
            //nowi split the text using split function and id is in first index 
            string[] splitText = readText.Split("at");
            // now trim the text from front and back 
            string[] trimmedText = splitText[1].Trim().Split(" ");
            Assert.AreEqual(email, trimmedText[0]);
            driver.Value.SwitchTo().Window(parentWindow);
            driver.Value.FindElement(By.Id("username")).SendKeys(trimmedText[0]);
        }

    }

}