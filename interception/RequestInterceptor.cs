using Castle.DynamicProxy;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Example
{
    public class RequestInterceptor : IInterceptor
    {
        private readonly Stopwatch stopwatch = new Stopwatch();

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
                    stopwatch.Start();
                    invocation.Proceed();

                    ((Task)invocation.ReturnValue).ContinueWith(_ => {
                        stopwatch.Stop();
                        Console.WriteLine($"Get took {stopwatch.ElapsedMilliseconds} milliseconds");
                    });
                    
                    break;
                    
                case "GetSync":
                    stopwatch.Start();
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
