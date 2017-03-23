#region Usings

using System.Collections.Generic;
using System.Linq;
using Jerboa.Engine.Custom;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

#endregion

namespace Jerboa.Pages.Google
{
    public class GoogleStartPage : BasePage
    {
        [FindsBy(How = How.XPath, Using = "//div[@class='g']//h3")]
        private IList<IWebElement> Results { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='lst-ib']")]
        private IWebElement SearchField { get; set; }

        public bool ResultsContain(string resultTitle)
        {
            return Results.Any(a => a.Text == resultTitle);
        }

        public void Search(string keyword)
        {
            SearchField.SendKeys(keyword);
        }
    }
}