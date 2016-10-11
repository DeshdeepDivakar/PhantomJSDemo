using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhantomJSDemo.Page;
using OpenQA.Selenium.Support.PageObjects;
using PhantomJSDemo.CorePackage;

namespace PhantomJSDemo.Page
{
    public class JourneyPlannerPage : BasePage
    {
        [FindsBy(How = How.XPath, Using = "//input[@id='name_origin']")]
        public IWebElement JourneyFrom;

        [FindsBy(How = How.XPath, Using = "//input[@id='name_destination']")]
        public IWebElement JourneyTo;

        [FindsBy(How = How.XPath, Using = "//li[@id='navMapJourney']//a")]
        public IWebElement JourneyPlannerLink;

        [FindsBy(How = How.XPath, Using = "//input[@id='jpSubmit']")]
        public IWebElement ShowJourney;


        public JourneyPlannerPage(IWebDriver webdriver)
            : base(webdriver)
        {
            PageFactory.InitElements(webdriver, this);           
        }


        public void ClickOnJourneyPlaner()
        {
            Initialiser.WaitForElementDisplayed(JourneyPlannerLink);
            Initialiser.TakeScreenshot("Test05.png", @"C:\TCM\QA\Automation\PhantomJSDemo\PhantomJSDemo\TestOutput\", true);
            JourneyPlannerLink.Click();
        }

        public void EnterJourneyDetails()
        {
            Initialiser.WaitForElementDisplayed(JourneyFrom);
            JourneyFrom.SendKeys("355 Spencer Street West Melbourne");
            Initialiser.TakeScreenshot("Test03.png", @"C:\TCM\QA\Automation\PhantomJSDemo\PhantomJSDemo\TestOutput\", true);
            JourneyTo.SendKeys("18B Crown Street Laverton");
            Initialiser.TakeScreenshot("Test04.png", @"C:\TCM\QA\Automation\PhantomJSDemo\PhantomJSDemo\TestOutput\", true);
            ShowJourney.Click();
        }
    }
}
