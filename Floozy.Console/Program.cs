using System;
using Floozy;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Floozy.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var testClass = new SampleTest();
            testClass.SearchNuGet();
            System.Console.ReadKey();
        }
    }

    public class SampleTest
    {     
        public void SearchNuGet()    
        {            
            using(var webDriver = new ChromeDriver(@"C:\Users\Vincent\source\repos\Floozy\Floozy.Console\bin\Debug\netcoreapp2.0"))
            {
                webDriver
                    .Load<NugetHomePage>("https://www.nuget.org/")
                    .Type("Selenium.WebDriver").Into(p => p.SearchBox)
                    .Click(p => p.SearchButton)
                    .Expect<SearchResultsPage>()
                    .Assert(p => p.TopResult)
                    .ContainsText("barry");
            }
        }
    }

    public class NugetHomePage : PageModelBase
    {
        public ElementLocator SearchBox => FindById("search");
        public ElementLocator SearchButton => FindByCss("#skippedToContent > section > div.jumbotron.text-center > div.container > div:nth-child(2) > div > form > div.input-group > span > button");
    }

    public class SearchResultsPage : PageModelBase
    {
        public ElementLocator TopResult => FindByCss("a.package-title");
    }
}
