using System;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MvcRouteTester.AspNetCore.Internal
{
    public static class MethodInfoExtensions
    {
        public static string GetActionText(this MethodInfo self, Func<Type, string> getTypeName)
        {
            if (self == null)
            {
                throw new ArgumentNullException(nameof(self));
            }

            if (getTypeName == null)
            {
                throw new ArgumentNullException(nameof(getTypeName));
            }

            var builder = new StringBuilder();
            builder.Append(getTypeName(self.ReflectedType));
            builder.Append('.');
            builder.Append(self.Name);
            builder.Append('(');
            builder.Append(string.Join(",", self
                .GetParameters()
                .Select(p => getTypeName(p.ParameterType))));
            builder.Append(')');
            return builder.ToString();
        }
    }
}
