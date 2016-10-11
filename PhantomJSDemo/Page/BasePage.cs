using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhantomJSDemo.Page
{
    public class  BasePage
    {
        public RemoteWebDriver driver;
        public BasePage(IWebDriver webDriver)
        {
            this.driver = (RemoteWebDriver)webDriver;
        }
    }
}
