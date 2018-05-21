using System;
using Floozy;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;

namespace Floozy.Sample
{
    public class SampleTest
    {
        [Fact]
        public void SearchNuGet()    
        {
            var webDriver = new ChromeDriver();
            webDriver
                .Load<NugetHomePage>("https://www.nuget.org/")
                .Type("Selenium.WebDriver").Into(p => p.SearchBox)
                .Click(p => p.SearchButton)
                .Expect<SearchResultsPage>()
                .Assert(p => p.TopResult)
                .ContainsText("Selenium.WebDriver");
        }
    }

    public class NugetHomePage : PageModelBase
    {
        public ElementLocator SearchBox => FindById("search");
        public ElementLocator SearchButton => FindByCss("btn-search");
    }

    public class SearchResultsPage : PageModelBase
    {
        public ElementLocator TopResult => FindByCss("a.package-title");
    }
}
