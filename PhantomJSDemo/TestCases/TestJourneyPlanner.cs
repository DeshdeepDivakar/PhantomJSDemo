using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhantomJSDemo.Page;
using PhantomJSDemo.CorePackage;
using System.Configuration;
using OpenQA.Selenium.Support.UI;

namespace PhantomJSDemo.TestCases
{
    [TestClass]
    public class TestJourneyPlanner
    {
        [TestInitialize]
        public void Setup()
        {
            var browserType = Enums.BrowserType.Chrome.ToString();
            
            PageFactories.InitializePages(Initialiser.GetDriver(browserType));
            Initialiser.Maximize();
            Initialiser.Open("http://ptv.vic.gov.au/");
            Initialiser.TakeScreenshot("Test01.png", @"C:\TCM\QA\Automation\PhantomJSDemo\PhantomJSDemo\TestOutput\", true);
        }

        [TestMethod]
        public void JourneyPlannerTest()
        {
            PageFactories.JourneyPlanner.ClickOnJourneyPlaner();
            Initialiser.TakeScreenshot("Test02.png",@"C:\TCM\QA\Automation\PhantomJSDemo\PhantomJSDemo\TestOutput\", true);
            PageFactories.JourneyPlanner.EnterJourneyDetails();            
        }
        [TestCleanup]
        public void TearDown()
        {
            Initialiser.CloseDriver();
            Initialiser.QuitDriver();
        }
    }
}
