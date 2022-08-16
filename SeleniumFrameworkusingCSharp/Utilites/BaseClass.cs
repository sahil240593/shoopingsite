using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;

namespace SeleniumFrameworkusingCSharp.Utilites
{
    public class BaseClass
    {
       public ExtentReports extent;
       public ExtentTest test;
         string browserName;
        //How to generate reports

        [OneTimeSetUp]
        public void Setup()
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            extent = new ExtentReports();   
            string reportPath = projectDirectory + "//index.html";
            var htmlReporter = new ExtentHtmlReporter(reportPath);
            extent.AttachReporter(htmlReporter); 

        }
        //public IWebDriver driver;
        public ThreadLocal<IWebDriver> driver = new ThreadLocal<IWebDriver>();
        [SetUp]
        public void StartBrowser()
        {
             test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
            //configuration
            browserName = TestContext.Parameters["browserName"];
            
            if (browserName == null)
            {
                browserName = ConfigurationManager.AppSettings["Browser"];
            }
            initBrowser(browserName);

            driver.Value.Manage().Window.Maximize();
            //Implicit wait give for 5 second globally
            driver.Value.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Value.Url = "https://rahulshettyacademy.com/loginpagePractise/";
        }
        public IWebDriver getDriver()
        {
            return driver.Value;
        }
        public void initBrowser(string BrowserName)
        {
            switch (BrowserName)
            {
                case "Chrome":
                    new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
                    driver.Value = new ChromeDriver();
                    break;

                case "Firefox":
                    new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
                    driver.Value = new FirefoxDriver();
                    break;

                case "Edge":
                    new WebDriverManager.DriverManager().SetUpDriver(new EdgeConfig());
                    driver.Value = new EdgeDriver();
                    break;



            }
        }

        public static jsonReader getDataParser()
        {
            return new jsonReader();
        }


        [TearDown]
        public void AfterTest()
        {
           var status= TestContext.CurrentContext.Result.Outcome.Status;
            var stackTrace = TestContext.CurrentContext.Result.StackTrace;  
            DateTime time = DateTime.Now;
            string filename = "screenshot_"+time.ToString("hh_mm_ss")+ ".png";

            if (status == TestStatus.Failed)
            {
                test.Fail("test failed", captureScreenshot(driver.Value,filename));
                test.Log(Status.Fail ,"test failed with logtrace"+stackTrace);  
            }
            else if (status == TestStatus.Passed) ;

            extent.Flush();
           driver.Value.Quit();  
        }
      public MediaEntityModelProvider captureScreenshot(IWebDriver driver, string screenshotName)
        {
            ITakesScreenshot ss = (ITakesScreenshot)driver;
            var screenshot = ss.GetScreenshot().AsBase64EncodedString;
            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot, screenshotName).Build();
        }

    }
}