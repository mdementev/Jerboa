#region Usings

using System;
using System.Collections.ObjectModel;
using System.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Internal;
using OpenQA.Selenium.Support.UI;

#endregion

namespace Jerboa.Engine
{
    public class SmartDriver : IWebDriver, IWrapsDriver
    {
        private static SmartDriver _instance = new SmartDriver();

        public SmartDriver()
        {
            var defaultExplicitWait = TimeSpan.FromSeconds(int.Parse(ConfigurationManager.AppSettings["DefaultTimeout"]));
            var service = ChromeDriverService.CreateDefaultService();
            service.HideCommandPromptWindow = true;

            var op = new ChromeOptions();
            op.AddArgument("--start-maximized");

            WrappedDriver = new ChromeDriver(service, op,
                defaultExplicitWait);
            WrappedDriver.Navigate().GoToUrl("http://google.com");

            WebDriverWaitInterval = new WebDriverWait(WrappedDriver, defaultExplicitWait);
            WebDriverWaitInterval.IgnoreExceptionTypes(typeof(NoSuchElementException),
                typeof(ElementNotVisibleException),
                typeof(StaleElementReferenceException),
                typeof(InvalidOperationException));

            WrappedDriver.Manage().Timeouts().ImplicitWait = defaultExplicitWait;
        }

        public static SmartDriver Instance => _instance ?? (_instance = new SmartDriver());

        private WebDriverWait WebDriverWaitInterval { get; }

        private IWebDriver WrappedDriver { get; }

        public void Dispose()
        {
            WrappedDriver.Close();
            WrappedDriver.Quit();
            _instance = null;
        }

        public IWebElement FindElement(By by)
        {
            IWebElement element = null;
            DoUntil(() => { element = WrappedDriver.FindElement(by); });

            return element;
        }

        ReadOnlyCollection<IWebElement> ISearchContext.FindElements(By by)
        {
            ReadOnlyCollection<IWebElement> elements = null;
            DoUntil(() => { elements = WrappedDriver.FindElements(by); });

            return elements;
        }

        void IWebDriver.Close()
        {
            WrappedDriver.Close();
        }

        string IWebDriver.CurrentWindowHandle => WrappedDriver.CurrentWindowHandle;

        IOptions IWebDriver.Manage()
        {
            return WrappedDriver.Manage();
        }

        public INavigation Navigate()
        {
            return WrappedDriver.Navigate();
        }

        string IWebDriver.PageSource => WrappedDriver.PageSource;

        void IWebDriver.Quit()
        {
            WrappedDriver.Quit();
        }

        public ITargetLocator SwitchTo()
        {
            return WrappedDriver.SwitchTo();
        }

        public string Title => WrappedDriver.Title;

        string IWebDriver.Url
        {
            get { return WrappedDriver.Url; }
            set { WrappedDriver.Url = value; }
        }

        public ReadOnlyCollection<string> WindowHandles => WrappedDriver.WindowHandles;

        IWebDriver IWrapsDriver.WrappedDriver => WrappedDriver;

        public void DoUntil(Action act)
        {
            try
            {
                WebDriverWaitInterval.Until(a =>
                {
                    act.Invoke();
                    return true;
                });
            }
            catch (Exception e)
            {
                while (e.InnerException != null)
                {
                    e = e.InnerException;
                }

                throw;
            }
        }

        public object ExecuteJavaScript(string script)
        {
            return ((IJavaScriptExecutor) WrappedDriver).ExecuteScript(script);
        }
    }
}