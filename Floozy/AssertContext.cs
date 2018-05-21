using System;
using System.Linq.Expressions;
using OpenQA.Selenium;

namespace Floozy
{
    public class AssertContext<TPageModel> where TPageModel : class, new()
    {
        private readonly IWebElement _element;
        private readonly PageContext<TPageModel> _pageContext;

        public AssertContext(PageContext<TPageModel> pageContext, IWebElement webElement)
        {
            _pageContext = pageContext ?? throw new ArgumentNullException(nameof(pageContext));
            _element = webElement ?? throw new ArgumentNullException(nameof(webElement));
        }

        public PageContext<TPageModel> ContainsText(string text)
        {
            if(!_element.Text.Contains(text))
                throw new InvalidOperationException($"Assertion faliled - search text '{text}', target text '{_element.Text}'");
            return _pageContext;
        }
    }
}