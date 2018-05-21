using OpenQA.Selenium;

namespace Floozy
{
     public static class DriverContext
    {
        public static PageContext<TPageModel> Load<TPageModel> (this IWebDriver webDriver, string url) where TPageModel : class, new()
        {
            webDriver.Url = url;
            return webDriver.With<TPageModel>();
        }        

        public static PageContext<TPageModel> With<TPageModel>(this IWebDriver webDriver) where TPageModel : class, new()
        {
            var pageModel = System.Activator.CreateInstance<TPageModel>();            
            return new PageContext<TPageModel>(webDriver, pageModel);
        }
    }    
}