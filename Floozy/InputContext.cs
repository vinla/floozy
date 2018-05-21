using System;
using System.Linq.Expressions;
using OpenQA.Selenium;

namespace Floozy
{
     public class InputContext<TPageModel> where TPageModel : class, new()
    {
        private readonly PageContext<TPageModel> _page;
        private readonly string _text;
        public InputContext(PageContext<TPageModel> page, string text)
        {
            _text = text;
            _page = page ?? throw new ArgumentNullException(nameof(page));            
        }

        public PageContext<TPageModel> Into<TProperty>(Expression<Func<TPageModel, TProperty>> property)
        {
            var propertyInfo = property.ToPropertyInfo();
            var elementLocator = propertyInfo.GetValue(_page.PageModel) as ElementLocator;
            var element = elementLocator(_page.WebDriver);
            element.SendKeys(_text);
            return _page;
        }
    }
}