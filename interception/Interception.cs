using Castle.DynamicProxy;
using System;
using System.Linq;
using System.Reflection;

namespace Example
{
    public static class Interception
    {
        public static object Intercept<TInterceptor>(Type type, IServiceProvider serviceProvider)
            where TInterceptor : IInterceptor, new()
        {
            return new ProxyGenerator().CreateClassProxy(
                type,
                GetCtorArgs(type, serviceProvider),
                new TInterceptor());
        }

        private static object[] GetCtorArgs(Type type, IServiceProvider serviceProvider)
        {
            return type
                .GetConstructors(BindingFlags.Public | BindingFlags.Instance)[0]
                .GetParameters()
                .Select(p => serviceProvider.GetService(p.ParameterType))
                .ToArray();
        }
    }
}
