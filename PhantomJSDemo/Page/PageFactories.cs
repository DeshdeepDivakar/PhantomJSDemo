using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhantomJSDemo.CorePackage;

namespace PhantomJSDemo.Page
{
    public class PageFactories
    {
        public static RemoteWebDriver _remoteWebDriver;
        public static TravelUpdatePage TravelUpdate;
        public static JourneyPlannerPage JourneyPlanner;

        public static void InitializePages(RemoteWebDriver driver)
        {
            _remoteWebDriver = driver;
            TravelUpdate = new TravelUpdatePage(driver);
            JourneyPlanner=new JourneyPlannerPage(driver);
        }
        public static RemoteWebDriver GetRemoteWebDriver()
        {
            return _remoteWebDriver;
        }
    }
}
