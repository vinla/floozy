using System;
using System.Linq.Expressions;
using OpenQA.Selenium;

namespace Floozy
{
    public class PageContext<TPageModel> where TPageModel : class, new()
    {
        private readonly IWebDriver _webDriver;
        private readonly TPageModel _pageModel;

        public PageContext(IWebDriver webDriver, TPageModel pageModel)
        {
            _webDriver = webDriver ?? throw new ArgumentNullException(nameof(webDriver));
            _pageModel = pageModel ?? throw new ArgumentNullException(nameof(pageModel));
        }    

        public IWebDriver WebDriver => _webDriver;

        public object PageModel => _pageModel;    

        public InputContext<TPageModel> Type(string keysToType)
        {
            return new InputContext<TPageModel>(this, keysToType);
        }

        public PageContext<TPageModel> Click<TProperty>(Expression<Func<TPageModel, TProperty>> property) 
        {            
            var propertyInfo = property.ToPropertyInfo();
            var elementLocator = propertyInfo.GetValue(_pageModel) as ElementLocator;
            var element = elementLocator(_webDriver);
            element.Click();            
            return this;
        }

        public PageContext<TNextPageModel> Expect<TNextPageModel>() where TNextPageModel : class, new()
        {
            var pageModel = System.Activator.CreateInstance<TNextPageModel>();            
            return new PageContext<TNextPageModel>(_webDriver, pageModel);
        }

        public AssertContext<TPageModel> Assert<TProperty>(Expression<Func<TPageModel, TProperty>> property)
        {
            var propertyInfo = property.ToPropertyInfo();
            var elementLocator = propertyInfo.GetValue(_pageModel) as ElementLocator;
            var element = elementLocator(_webDriver);
            return new AssertContext<TPageModel>(this, element);
        }
    }
}