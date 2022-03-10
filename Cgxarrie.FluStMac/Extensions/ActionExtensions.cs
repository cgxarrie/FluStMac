namespace Cgxarrie.FluStMac.Extensions
{
    using System.Linq.Expressions;

    internal static class ActionExtensions
    {
        public static string GetName<T>(this Expression<Action<T>> action)
        {
            return ((MethodCallExpression)action.Body).Method.Name;
        }
    }
}