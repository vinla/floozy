using System;
using OpenQA.Selenium;

namespace Floozy
{
    public class PageModelBase
    {
        public ElementLocator FindById(string id)
        {
            return new ElementLocator((d) => d.FindElement(By.Id(id)) );
        }

        public ElementLocator FindByCss(string cssSelector)
        {
            return new ElementLocator(d => d.FindElement(By.CssSelector(cssSelector)));
        }
    }    
}