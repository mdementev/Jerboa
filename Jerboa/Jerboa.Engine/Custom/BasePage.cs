#region Usings

using OpenQA.Selenium.Support.PageObjects;

#endregion

namespace Jerboa.Engine.Custom
{
    public class BasePage
    {
        public BasePage()
        {
            PageFactory.InitElements(SmartDriver.Instance, this);
        }
    }
}