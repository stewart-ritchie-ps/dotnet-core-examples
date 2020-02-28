using Castle.DynamicProxy;
using System;
using System.Diagnostics;

namespace Example
{
    public class RequestInterceptor : IInterceptor
    {
        private static readonly Stopwatch stopwatch = new Stopwatch();

        public void Intercept(IInvocation invocation)
        {
            if (!typeof(Request).IsAssignableFrom(invocation.TargetType))
            {
                invocation.Proceed();
                return;
            }

            switch (invocation.Method.Name)
            {
                case "Get":
                    stopwatch.Restart();
                    invocation.Proceed();
                    stopwatch.Stop();
                    Console.WriteLine($"Get took {stopwatch.ElapsedMilliseconds} milliseconds");
                    break;

                default:
                    invocation.Proceed();
                    break;
            }
        }
    }
}
