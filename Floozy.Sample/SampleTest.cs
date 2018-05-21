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
        public ElementLocator SearchBox => new ElementLocator(d => d.FindElement(By.Id("search")));
        public ElementLocator SearchButton => new ElementLocator(d => d.FindElement(By.CssSelector("btn-search")));
    }

    public class SearchResultsPage : PageModelBase
    {
        public ElementLocator TopResult => new ElementLocator(d => d.FindElement(By.CssSelector("a.package-title")));
    }
}
