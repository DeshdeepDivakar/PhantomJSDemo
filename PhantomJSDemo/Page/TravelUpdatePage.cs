using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhantomJSDemo.Page;
using OpenQA.Selenium.Support.PageObjects;

namespace PhantomJSDemo.Page
{
    public class TravelUpdatePage:BasePage
    {
        [FindsBy(How=How.XPath, Using=("//"))]
        public IWebElement WelcomeText;

        public TravelUpdatePage(IWebDriver webdriver): base(webdriver) {
            PageFactory.InitElements(webdriver, this);           
        }
        

    }
}
