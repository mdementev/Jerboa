#region Usings

using Jerboa.Engine;
using TechTalk.SpecFlow;

#endregion

namespace Jerboa.Tests.Steps
{
    [Binding]
    [Scope(Tag = "Common")]
    internal static class Common
    {
        [AfterScenario]
        public static void Teardown()
        {
            SmartDriver.Instance.Dispose();
        }
    }
}