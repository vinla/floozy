using System;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;

namespace Floozy
{
    internal static class ExpressionExtensions
    {
        public static PropertyInfo ToPropertyInfo(this LambdaExpression expression)
        {
            MemberExpression prop;
            PropertyInfo info;
            if ((prop = (expression.Body as MemberExpression)) != null && (object)(info = (prop.Member as PropertyInfo)) != null)
            {
                if (info.DeclaringType != prop.Expression.Type && info.CanRead)
                {
                    PropertyInfo propertyInLeft = prop.Expression.Type.GetProperty(info.Name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                    if (propertyInLeft != (PropertyInfo)null && propertyInLeft.GetMethod.GetBaseDefinition() == info.GetMethod)
                    {
                        info = propertyInLeft;
                    }
                }
                return info;
            }
            
            throw new ArgumentException();
        }
    }
}