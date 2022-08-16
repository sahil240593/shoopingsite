using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumFrameworkusingCSharp.PageObject
{
    public class LoginPage
    {
        private IWebDriver driver;
        

        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver,this);
        }
        [FindsBy(How = How.Id, Using = "username")]
        private IWebElement username;
        [FindsBy(How =How.Name, Using = "password")]
        private IWebElement password;  
        [FindsBy(How =How.XPath, Using = "//div[@class = 'form-group'][5]/label/span/input")]
        private IWebElement checkbox;
        [FindsBy(How = How.XPath, Using = "//input[@id='signInBtn']")]
        private IWebElement signinbutton;

        public void validLogin(string user,string pass)
        {
            username.SendKeys(user);
            password.SendKeys(pass);
            checkbox.Click();   
            signinbutton.Click();   
        }
        
        public IWebElement getUserName()
        {
            return username;
        }
    }
}
