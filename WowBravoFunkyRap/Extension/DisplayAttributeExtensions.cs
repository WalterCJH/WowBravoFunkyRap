using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;

namespace WowBravoFunkyRap.Extension
{
    public static class DisplayAttributeExtensions
    {
        public static string GetDisplayName(this Type type)
        {
            var displayAttribute = type.GetCustomAttribute<DisplayAttribute>();
            return displayAttribute?.Name ?? type.Name;
        }

        public static string GetDisplayName(this PropertyInfo propertyInfo)
        {
            var displayAttribute = propertyInfo.GetCustomAttribute<DisplayAttribute>();
            return displayAttribute?.Name ?? propertyInfo.Name;
        }

        public static string GetDisplayName(this object obj)
        {
            return obj.GetType().GetDisplayName();
        }

        public static string GetDisplayName<T>(this T instance, Expression<Func<T, object>> expression)
        {
            if (expression.Body is MemberExpression member)
            {
                var displayAttribute = member.Member.GetCustomAttribute<DisplayAttribute>();
                return displayAttribute?.Name ?? member.Member.Name;
            }
            else if (expression.Body is UnaryExpression unary && unary.Operand is MemberExpression unaryMember)
            {
                var displayAttribute = unaryMember.Member.GetCustomAttribute<DisplayAttribute>();
                return displayAttribute?.Name ?? unaryMember.Member.Name;
            }
            return null;
        }
    }
}
