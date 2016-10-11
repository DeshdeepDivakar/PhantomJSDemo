using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing.Imaging;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System.Drawing;
using OpenQA.Selenium.Interactions;
using System.Collections;
using System.Net;
using OpenQA.Selenium.PhantomJS;


namespace PhantomJSDemo.CorePackage
{
    public class Initialiser
    {
        private static RemoteWebDriver _remoteWebDriver;
        public static RemoteWebDriver GetDriver(string browserName, int timeOut = 3)
        {

            switch (browserName)
            {
                case "IE":
                    //configure node to run Internet Explorer
                    _remoteWebDriver= new InternetExplorerDriver();
                    break;
                case "firefox":
                    //configure node to run FireFox
                   // FirefoxBinary ffBinary = new FirefoxBinary(@"C:\Program Files (x86)\Mozilla Firefox\firefox.exe");
                    //FirefoxProfile profile = new FirefoxProfile(@"C:\FFProfiles\Automation\");
                    //DesiredCapabilities capabilitiesFF = DesiredCapabilities.Firefox();
                    //capabilitiesFF.SetCapability("platform", "WINDOWS");
                    //capabilitiesFF.SetCapability("binary", @"C:\Program Files (x86)\Mozilla Firefox\firefox.exe");   
                    //capabilitiesFF.SetCapability(CapabilityType.IsJavaScriptEnabled, true);
                    //capabilitiesFF.SetCapability("marionette", true);
                   // _remoteWebDriver = new FirefoxDriver(ffBinary,null,TimeSpan.FromSeconds(5));
                    FirefoxProfile ff  = new FirefoxProfile("default"); 
                   _remoteWebDriver = new FirefoxDriver(ff);
                    break;
                case "Chrome":
                    //configure node to run Chrome
                    var options = new ChromeOptions();
                    var capabilities = (DesiredCapabilities)options.ToCapabilities();
                    capabilities.SetCapability(CapabilityType.Platform, "WINDOWS");
                    capabilities.SetCapability(CapabilityType.IsJavaScriptEnabled, true);
                    _remoteWebDriver = new ChromeDriver(@"C:\TCM\QA\Automation\PhantomJSDemo\packages\Chromium.ChromeDriver.2.20\content\");
                    break;
                case "PhantomJS":
                    //configure node to run PhantomJS
                    _remoteWebDriver = new PhantomJSDriver();
                    break;
                default:
                    //Console.Write("gridUrl=" + gridUrl + "  ----browserName:" + browserName);
                    //var options = new ChromeOptions();
                    //options.AddArgument("--user-agent=" + userAgent);
                    //options.AddUserProfilePreference("download.prompt_for_download", false);
                    //var capabilities = (DesiredCapabilities)options.ToCapabilities();
                    //capabilities.SetCapability(CapabilityType.Platform, "WINDOWS");
                    //capabilities.SetCapability(CapabilityType.IsJavaScriptEnabled, true);
                    //_remoteWebDriver = new RemoteWebDriver(new Uri(gridUrl), capabilities);
                    break;
            }

            _remoteWebDriver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(timeOut));
            return _remoteWebDriver;

        }

        public static void Open(string url)
        {
            _remoteWebDriver.Navigate().GoToUrl(url);

        }

        public static void Maximize()
        {
            _remoteWebDriver.Manage().Window.Maximize();
        }

        public static void ResizeWindow(int x, int y)
        {
            _remoteWebDriver.Manage().Window.Size = new Size(x, y);
        }
      
        public static void CloseDriver()
        {
            if (_remoteWebDriver != null)
            {
                _remoteWebDriver.Close();
            }
        }

        public static void QuitDriver()
        {
            if (_remoteWebDriver != null)
            {
                _remoteWebDriver.Quit();
            }
        }

        public static bool WaitForElementEnabled(IWebElement element, int timeOut = 10)
        {
            var isEnabled = true;
            try
            {
                var wait = new WebDriverWait(_remoteWebDriver, TimeSpan.FromSeconds(timeOut));
                wait.Until(d => element.Enabled);
            }
            catch (Exception ex)
            {
                isEnabled = false;
                Console.WriteLine("Exception while waiting for webelement " + ex);
            }
            return isEnabled;
        }

        public static bool WaitForElementDisplayed(IWebElement element, int timeOut = 10)
        {
            var isDisplayed = true;
            try
            {
                var wait = new WebDriverWait(_remoteWebDriver, TimeSpan.FromSeconds(timeOut));
                wait.Until(d => element.Displayed);
            }
            catch (Exception ex)
            {
                isDisplayed = false;
                //Console.WriteLine("Exception while waiting for webelement " + ex);
            }
            return isDisplayed;
        }

        public static bool WaitForElementNotDisplayed(IWebElement element, int timeOut = 10)
        {
            var isNotDisplayed = true;
            try
            {
                var wait = new WebDriverWait(_remoteWebDriver, TimeSpan.FromSeconds(timeOut));
                wait.Until(d => !element.Displayed);
            }
            catch (Exception ex)
            {
                isNotDisplayed = false;
                //Console.WriteLine("Exception while waiting for webelement " + ex);
            }
            return isNotDisplayed;
        }

        public static String WaitAndGetText(IWebElement element, int timeOut = 10)
        {

            WaitForElementDisplayed(element, timeOut);
            return element.Text;
        }

        public static void WaitAndClick(IWebElement element, int timeOut = 10)
        {
            WaitForElementEnabled(element, timeOut);
            element.Click();
        }

        public static void WaitAndType(IWebElement element, string text, int timeOut = 10)
        {
            WaitForElementEnabled(element, timeOut);
            element.Clear();
            element.SendKeys(text);
        }

        public static void WaitAndClear(IWebElement element, int timeOut = 10)
        {
            WaitForElementEnabled(element, timeOut);
            element.Clear();
        }

        public static string WaitAndGetAttribute(IWebElement element, string attribute, int timeOut = 10)
        {
            WaitForElementDisplayed(element, timeOut);
            return element.GetAttribute(attribute);
        }

        public static IWebElement WaitAndFindElement(How how, string selector, int timeOut = 10)
        {
            IWebElement element = null;
            WebDriverWait wait = new WebDriverWait(_remoteWebDriver, TimeSpan.FromSeconds(timeOut));
            try
            {
                switch (how)
                {
                    case How.CssSelector:
                        element = wait.Until(ExpectedConditions.ElementExists(By.CssSelector(selector)));
                        break;
                    case How.XPath:
                        element = wait.Until(ExpectedConditions.ElementExists(By.XPath(selector)));
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.Write("Unable to locate element using " + selector);
            }

            return element;
        }

        public static void TakeScreenshot(String fileName, String directory, bool takeScreenshot = true)
        {
            try
            {
                if (takeScreenshot)
                {
                    var dateString = DateTime.Now.Day + "_" + DateTime.Now.Month + "_" + DateTime.Now.Year;
                    directory = directory.Replace("DATE", dateString);
                    if (!Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }

                    var fileFullPath = directory + Path.DirectorySeparatorChar + fileName;
                    Screenshot screenShot = ((ITakesScreenshot)_remoteWebDriver).GetScreenshot();
                    screenShot.SaveAsFile(fileFullPath, ImageFormat.Png);
                    Console.Write("Screenshot Saved\n" + fileFullPath);
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while taking screenshot: {0}", ex);
            }
        }

        public static void WaitAndSelectByText(IWebElement element, string text, int timeOut = 10)
        {
            WaitForElementEnabled(element);
            SelectElement selectElem = new SelectElement(element);
            selectElem.SelectByText(text);
        }

        public static void WaitAndSelectByIndex(IWebElement element, int index, int timeOut = 10)
        {
            WaitForElementEnabled(element);
            SelectElement selectElem = new SelectElement(element);
            selectElem.SelectByIndex(index);
        }

        public static string WaitAndGetSelectedValue(IWebElement element, int timeOut = 10)
        {
            WaitForElementEnabled(element);
            SelectElement selectElem = new SelectElement(element);
            return selectElem.SelectedOption.Text;
        }

        public static void HoverOnElement(IWebElement element, IWebDriver driver)
        {
            Actions action = new Actions(driver);
            action.MoveToElement(element).Build().Perform();
        }

        public static void MoveAndClick(IWebElement element, IWebDriver driver, int x = 0, int y = 0)
        {
            Actions action = new Actions(driver);
            if (element == null)
            {
                action.MoveByOffset(x, y).Click().Build().Perform();
            }
            else
            {
                action.MoveToElement(element, x, y).Click().Build().Perform();
            }
        }

        public static void ClickUsingAction(IWebElement element, IWebDriver driver)
        {
            Actions action = new Actions(driver);
            action.MoveToElement(element).Click().Perform();

        }

        public static void SwitchWindowByPosition(int pos)
        {
            _remoteWebDriver.SwitchTo().Window(_remoteWebDriver.WindowHandles[pos]);

        }

        public static void SwitchWindowByTitle(string title)
        {
            foreach (var window in _remoteWebDriver.WindowHandles)
            {
                if (_remoteWebDriver.SwitchTo().Window(window).Title.Equals(title))
                {
                    _remoteWebDriver.SwitchTo().Window(window);
                }
            }
        }

        public static bool IsTextPresent(string text, string tag = "")
        {
            string xpath = "//*[contains(text(),'" + text + "')]";
            xpath = tag.Equals("") ? xpath : xpath.Replace("*", tag);
            return (_remoteWebDriver.FindElementsByXPath(xpath).Count != 0);
        }

        public static bool IsTextPresentWithSpecialCharacter(string text, string tag = "")
        {
            string xpath = "//*[contains(text()," + text + ")]";
            xpath = tag.Equals("") ? xpath : xpath.Replace("*", tag);
            return (_remoteWebDriver.FindElementsByXPath(xpath).Count != 0);
        }

        public static void ScrollElementIntoView(IWebElement element)
        {
            ((IJavaScriptExecutor)_remoteWebDriver).ExecuteScript("arguments[0].scrollIntoView(true);", element);
        }

        //public static void ScrollUpAndScrollIntoView(IWebElement element)
        //{
        //    ((IJavaScriptExecutor)_remoteWebDriver).ExecuteScript("window.scrollTo(0,0);", element);
        //    Actions actions = new Actions(MCWebDriver._remoteWebDriver);
        //    actions.MoveToElement(element);
        //    actions.Perform();
        //}

        public static int GetWindowHandlesCount()
        {
            return _remoteWebDriver.WindowHandles.Count;
        }
        /**
         * 
         * Checks if the list is sorted in descending order
         * 
         * */
        public static bool IsListSorted(ArrayList List)
        {
            bool flag = true;
            int I = 0;
            decimal F = 0;
            decimal S = 0;

            while (I < List.Count - 1)
            {
                F = (decimal)List[I];
                S = (decimal)List[I + 1];
                if (F < S)
                {
                    flag = false;
                    break;
                }
                I++;
            }
            return flag;
        }

        //HTTP HEAD request to check file is downloadable
        public static bool IsFileDownloadable(string url, string method = "HEAD", string contentType = "application/pdf")
        {
            bool isDownloadable = false;

            HttpWebRequest req = WebRequest.Create(url) as HttpWebRequest;
            req.Accept = contentType;
            req.Method = method;

            HttpWebResponse resp = req.GetResponse() as HttpWebResponse;
            if (((int)HttpStatusCode.OK) == (int)resp.StatusCode)
            {
                isDownloadable = resp.ContentType.Contains(contentType) && resp.ContentLength > 0;
            }

            resp.Dispose();

            return isDownloadable;

        }

    }
}
