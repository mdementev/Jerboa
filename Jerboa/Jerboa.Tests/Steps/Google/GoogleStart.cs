#region Usings

using FluentAssertions;
using Jerboa.Pages.Google;
using TechTalk.SpecFlow;

#endregion

namespace Jerboa.Tests.Steps.Google
{
    [Binding]
    [Scope(Tag = "Google.GoogleStart")]
    internal class GoogleStart
    {
        [Given(@"I search for (.*)")]
        public void GivenISearchFor(string keyword)
        {
            var page = new GoogleStartPage();
            page.Search(keyword);
        }

        [Then(@"Result is (.*)")]
        public void ThenResultIsJerboa_Wikipedia(string resultTitle)
        {
            var page = new GoogleStartPage();
            page.ResultsContain(resultTitle).Should().BeTrue();
        }
    }
}